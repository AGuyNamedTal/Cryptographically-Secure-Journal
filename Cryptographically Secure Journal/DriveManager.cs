using CryptographicallySecureJournal.Utils;
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

        private DriveService _service;
        public const string FileName = "journal.encrypted";
        private long? _fileSize;
        private string _fileId = null;

        public DriveManager(Action<bool> continueWith)
        {
            string[] scopes = { DriveService.Scope.DriveFile };
            string applicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            // Load client secrets.
            using (FileStream stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                /* The file token.json stores the user's access and refresh tokens, and is created
                 automatically when the authorization flow completes for the first time. */
                const string credPath = "token.json";
                GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.FromStream(stream).Secrets,
                   scopes,
                   "user",
                   CancellationToken.None,
                   new FileDataStore(credPath, true)).ContinueWith(task =>
               {
                   if (task.IsFaulted)
                   {
                       continueWith(false);
                       return;
                   }
                   // Create Drive API service.
                   _service = new DriveService(new BaseClientService.Initializer
                   {
                       HttpClientInitializer = task.Result,
                       ApplicationName = applicationName
                   });
                   continueWith(true);
               });
            }

        }

        public bool FindJournalFile()
        {
            FilesResource.ListRequest listRequest = _service.Files.List();
            listRequest.Q = $"name = '{FileName}'";
            listRequest.IncludeItemsFromAllDrives = false;
            //listRequest.Fields = "size";
            IList<File> fileList = listRequest.Execute().Files;
            if (fileList.Count == 0)
            {
                return false;
            }

            _fileId = fileList.First().Id;
            return true;
        }

        public void DownloadJournal(MemoryStream memoryStream, ProgressUpdater progress,
            Action<IDownloadProgress> onComplete)
        {
            FilesResource.GetRequest getRequest = _service.Files.Get(_fileId);
            getRequest.Fields = "size";
            _fileSize = getRequest.Execute().Size;
            getRequest.MediaDownloader.ProgressChanged += val =>
                {
                    progress.Update(val.BytesDownloaded / (double)_fileSize);
                };
            getRequest.DownloadAsync(memoryStream).ContinueWith(task => onComplete(task.Result));

        }

        public void UploadJournal(Journal journal, ProgressUpdater progressUpdater,
            Action<IUploadProgress> onComplete)
        {

            MemoryStream stream = journal.ToMemoryStream();
            File file = new File
            {
                Name = FileName
            };

            void UpdateProgressBar(IUploadProgress progress)
            {
                progressUpdater.Update((double)progress.BytesSent / stream.Length);
            }

            void ContinueWith(Task<IUploadProgress> task)
            {
                stream.Dispose();
                onComplete(task.Result);
            }

            if (_fileId == null)
            {
                // create
                FilesResource.CreateMediaUpload createRequest = _service.Files.Create(file,
                    stream, "application/unknown");
                createRequest.Fields = "id";
                createRequest.ProgressChanged += UpdateProgressBar;
                createRequest.UploadAsync().ContinueWith(task =>
                {
                    _fileId = createRequest.ResponseBody.Id;
                    ContinueWith(task);
                });
            }
            else
            {
                // update
                FilesResource.UpdateMediaUpload createRequest = _service.Files.Update(new File(), _fileId,
                    stream, "application/unknown");
                createRequest.ProgressChanged += UpdateProgressBar;
                createRequest.UploadAsync().ContinueWith(ContinueWith);
            }
        }

        public void DeleteJournal(Action<Task<string>> onComplete)
        {
            FilesResource.DeleteRequest deleteRequest = _service.Files.Delete(_fileId);
            deleteRequest.ExecuteAsync().ContinueWith(onComplete);
        }

        public void UploadBackup(Journal journal, string fileName, ProgressUpdater progressUpdater,
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
                progressUpdater.Update((double)progress.BytesSent / stream.Length);
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
