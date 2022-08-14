using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v3.Data.File;

namespace CryptographicallySecureJournal
{
    public class DriveManager
    {

        private readonly DriveService _service;
        public const string FileName = "journal.encrypted";
        private File _journalFile;

        public DriveManager()
        {
            string[] scopes = { DriveService.Scope.DriveFile };
            string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            UserCredential credential;
            // Load client secrets.
            using (FileStream stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                /* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
                const string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            // Create Drive API service.
            _service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
        }

        public bool FindJournalFile()
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = $"name = {FileName}";
            listRequest.IncludeItemsFromAllDrives = false;
            listRequest.Fields = "size";
            IList<File> fileList = listRequest.Execute().Files;
            if (fileList.Count == 0)
            {
                return false;
            }

            _journalFile = fileList.First();
            return true;
        }

        public void DownloadJournal(MemoryStream memoryStream, Action<int> progressBarUpdate,
            Action<IDownloadProgress> onComplete)
        {
            FilesResource.GetRequest getRequest = _service.Files.Get(_journalFile.Id);
            long fileSize = (long)_journalFile.Size;
            getRequest.MediaDownloader.ProgressChanged += progress =>
                {
                    progressBarUpdate((int)(progress.BytesDownloaded / (double)fileSize * 100));
                };
            getRequest.DownloadAsync(memoryStream).ContinueWith(task => onComplete(task.Result));

        }

        public void UploadJournal(Journal journal, Action<int> progressBarUpdate,
            Action<IUploadProgress> onComplete)
        {

            MemoryStream stream = journal.ToMemoryStream();
            File file = new File
            {
                Name = FileName
            };

            void UpdateProgressBar(IUploadProgress progress)
            {
                progressBarUpdate((int)(progress.BytesSent * 100d / stream.Length));
            }

            void ContinueWith(Task<IUploadProgress> task)
            {
                stream.Dispose();
                onComplete(task.Result);
            }

            if (_journalFile == null)
            {
                // create
                FilesResource.CreateMediaUpload createRequest = _service.Files.Create(file,
                    stream, "application/unknown");
                createRequest.ProgressChanged += UpdateProgressBar;
                createRequest.UploadAsync().ContinueWith(ContinueWith);
            }
            else
            {
                // update
                FilesResource.UpdateMediaUpload createRequest = _service.Files.Update(file, _journalFile.Id,
                    stream, "application/unknown");
                createRequest.ProgressChanged += UpdateProgressBar;
                createRequest.UploadAsync().ContinueWith(ContinueWith);
            }
        }

        public void DeleteJournal(Action<Task<string>> onComplete)
        {
            FilesResource.DeleteRequest deleteRequest = _service.Files.Delete(_journalFile.Id);
            deleteRequest.ExecuteAsync().ContinueWith(onComplete);
        }

        public void UploadBackup(Journal journal, string fileName, Action<int> updateProgress,
            Action<IUploadProgress> onComplete)
        {
            MemoryStream stream = journal.ToMemoryStream();
            FilesResource.CreateMediaUpload createRequest = _service.Files.Create(new File()
            {
                Name = fileName
            },
                stream, "application/unknown");
            createRequest.ProgressChanged += progress =>
            {
                updateProgress((int)(progress.BytesSent * 100d / stream.Length));
            };
            createRequest.UploadAsync().ContinueWith(task =>
            {
                try
                {
                    stream.Dispose();
                }
                catch
                {
                    // ignored
                }

                onComplete(task.Result);
            });
        }

    }
}
