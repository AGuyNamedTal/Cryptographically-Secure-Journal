using Google.Apis.Download;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class StartupForm : Form
    {
        private DriveManager _driveManager;
        private Journal _journal;
        public StartupForm()
        {
            InitializeComponent();
            SetEnabledOfJonControls(false);
        }

        private void ConnectToDriveBtnClick(object sender, EventArgs e)
        {
            try
            {
                _driveManager = new DriveManager();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error connecting with Google Drive",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_driveManager.FindJournalFile())
            {
                MessageBox.Show("Journal not found on Google Drive\n" +
                                "Please create a new journal", "Journal not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.SwitchForm(new NewJournalForm(_driveManager));
                return;
            }

            MemoryStream memoryStream = new MemoryStream();
            _driveManager.DownloadJournal(memoryStream, UpdateProgressBar, downloadProgress =>
            {
                if (downloadProgress.Status == DownloadStatus.Failed)
                {
                    MessageBox.Show(downloadProgress.Exception.Message ?? "Download failed",
                        "Error downloading journal from Google Drive", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    memoryStream.Dispose();
                    return;
                }

                try
                {
                    _journal = new Journal(memoryStream);
                    SetEnabledOfJonControls(true);
                }
                catch (Exception exception)
                {
                    if (MessageBox.Show($"Can't parse journal - {exception.Message ?? "No details"}\n" +
                                        $"Would you like to create a new journal?", "Journal Corrupt",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        this.SwitchForm(new NewJournalForm(_driveManager));
                    }
                }
                finally
                {
                    memoryStream.Dispose();
                }
            });



        }

        private void UpdateProgressBar(int value)
        {
            progressBar.Invoke(new Action(() => progressBar.Value = value));
        }


        private void ContinueBtnClick(object sender, EventArgs e)
        {
            new Thread(() => DecryptJournal(passwordTxtBox.Text)).Start();
        }

        private void DecryptJournal(string pass)
        {
            byte[] passHash = HashAndSalt.Password(Encoding.UTF8.GetBytes(pass), _journal.PassSalt);
            byte[] decryptedText;
            try
            {
                decryptedText = AesEncryption.Decrypt(_journal.EncryptedText, passHash);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message} - probably wrong password",
                    "Error decrypting journal", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            this.SwitchForm(new JournalEditorForm(
                Encoding.UTF8.GetString(decryptedText), passHash, _journal, _driveManager));
        }

        private void SecuritySettingsLblClick(object sender, EventArgs e)
        {
            new SecurityOptionsForm(_driveManager, _journal).ShowDialog();
        }

        private void SetEnabledOfJonControls(bool enabled)
        {
            Control[] controls = { securitySettingsLbl, continueBtn, passwordTxtBox };
            foreach (Control control in controls)
            {
                control.Enabled = enabled;
            }
        }
    }
}
