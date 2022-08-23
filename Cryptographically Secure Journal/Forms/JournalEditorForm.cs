using CryptographicallySecureJournal.Crypto;
using CryptographicallySecureJournal.Utils;
using Google.Apis.Upload;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class JournalEditorForm : Form
    {
        private readonly byte[] _passHash;
        private readonly DriveManager _driveManager;
        private readonly Journal _journal;
        private string _uploadedText;

        public JournalEditorForm(string text, byte[] passHash, Journal journal, DriveManager driveManager)
        {
            InitializeComponent();
            _uploadedText = text;
            txtBox.AppendText(text);
            txtBox.ScrollToBottom();
            _passHash = passHash;
            _driveManager = driveManager;
            _journal = journal;
        }

        private void JournalEditorFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_uploadedText != txtBox.Text && ShowExitWithoutUploading())
            {
                e.Cancel = true;
            }
        }

        private void UploadBtnClick(object sender, EventArgs e)
        {
            new Thread(() => UploadJournal(null)).Start();
        }

        private void UploadAndExitBtnClick(object sender, EventArgs e)
        {
            new Thread(() => UploadJournal(failed =>
            {
                if (failed)
                {
                    if (ShowExitWithoutUploading())
                    {
                        return;
                    }
                }

                this.CloseOnUIThread();
            })).Start();
        }

        private static bool ShowExitWithoutUploading()
        {
            return MessageBox.Show("Do you want to exit without uploading to drive?", "Quit?",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No;
        }

        private void UploadJournal(Action<bool> onComplete)
        {
            string textToUpload = txtBox.Text;
            if (_uploadedText == textToUpload)
            {
                onComplete?.Invoke(false);
                return;
            }
            _journal.EncryptedText = AesEncryption.Encrypt(Encoding.UTF8.GetBytes(textToUpload),
                _passHash);

            _driveManager.UploadJournal(_journal, progressBar.Updater, progress =>
            {
                bool failed = MsgBoxForFailedUpload(progress);
                if (!failed)
                {
                    _uploadedText = textToUpload;
                }
                onComplete?.Invoke(failed);
            });
        }

        private void UpdateProgressBar(int value)
        {
            progressBar.Invoke(new Action(() => progressBar.Value = value));

        }
        /// <summary>
        /// Checks uploadprogress and shows a msgbox if an error occurred
        /// </summary>
        /// <param name="uploadProgress"></param>
        /// <returns>Returns true if an error occurred during upload</returns>
        public static bool MsgBoxForFailedUpload(IUploadProgress uploadProgress)
        {
            if (uploadProgress.Status == UploadStatus.Failed)
            {
                MessageBox.Show(uploadProgress.Exception.Message ?? "Upload failed",
                    "Error uploading journal to Google Drive", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            return false;
        }
    }
}
