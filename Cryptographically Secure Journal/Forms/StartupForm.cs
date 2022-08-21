using CryptographicallySecureJournal.Utils;
using Google.Apis.Auth.OAuth2.Responses;
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
        public Journal Journal;
        public StartupForm()
        {
            InitializeComponent();
            SetEnabledOfJonControls(false);
        }
        private void StartupFormLoad(object sender, EventArgs e)
        {
            bool autoConnect = SettingsManager.AutoConnect;
            connectAutomaticallyCheckBox.Checked = autoConnect;
            if (autoConnect)
            {
                ConnectToDriveBtnClick(null, null);
            }
        }

        private void ConnectToDriveBtnClick(object sender, EventArgs e)
        {
            SetEnabledConnectDriveBtn(false);
            try
            {
                _driveManager = new DriveManager(success =>
                {

                    if (success)
                    {
                        FindAndDownloadJournal();
                    }
                    else
                    {
                        MessageBox.Show("Can't connect to Google Drive", "Error connecting with Google Drive",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetEnabledConnectDriveBtn(true);
                    }

                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error connecting with Google Drive",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetEnabledConnectDriveBtn(true);
                return;
            }
        }

        private void SetEnabledConnectDriveBtn(bool enabled)
        {
            if (connectToDriveBtn.InvokeRequired)
            {
                connectToDriveBtn.Invoke(new Action(() => connectToDriveBtn.Enabled = enabled));
            }
            else
            {
                connectToDriveBtn.Enabled = enabled;
            }
        }

        private void FindAndDownloadJournal()
        {
            try
            {
                if (!_driveManager.FindJournalFile())
                {
                    SetEnabledConnectDriveBtn(true);
                    MessageBox.Show("Journal not found on Google Drive\n" +
                                    "Please create a new journal", "Journal not found", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.SwitchForm(new NewJournalForm(_driveManager));
                    return;
                }
            }
            catch (TokenResponseException exception)
            {
                MessageBox.Show($"{exception.Message} - a token error has occurred, please try connecting again\n" +
                                $"The application will now restart.",
                    "Token Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Restart();
            }

            MemoryStream memoryStream = new MemoryStream();
            _driveManager.DownloadJournal(memoryStream, UpdateProgressBar, downloadProgress =>
            {
                if (downloadProgress.Status == DownloadStatus.Failed)
                {
                    MessageBox.Show(downloadProgress.Exception.Message ?? "Download failed",
                        "Error downloading journal from Google Drive", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    SetEnabledConnectDriveBtn(true);
                    memoryStream.Dispose();
                    return;
                }

                try
                {
                    memoryStream.Position = 0;
                    Journal = new Journal(memoryStream);
                }
                catch (Exception exception)
                {
                    SetEnabledConnectDriveBtn(true);
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
                SetEnabledOfJonControls(true);
                SetEnabledConnectDriveBtn(true);
            });

        }

        public void UpdateProgressBar(int value)
        {
            progressBar.Invoke(new Action(() => progressBar.Value = value));
        }


        private void ContinueBtnClick(object sender, EventArgs e)
        {
            new Thread(() => DecryptJournal(passwordTxtBox.Text)).Start();
        }

        private void DecryptJournal(string pass)
        {
            UpdateProgressBar(0);
            byte[] key = HashAndSalt.Password(Encoding.UTF8.GetBytes(pass), Journal.PassSalt);
            UpdateProgressBar(90);
            byte[] decryptedText;
            try
            {
                decryptedText = AesEncryption.Decrypt(Journal.EncryptedText, key);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message} - probably wrong password",
                    "Error decrypting journal", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            UpdateProgressBar(100);
            this.SwitchForm(() => new JournalEditorForm(
                Encoding.UTF8.GetString(decryptedText), key, Journal, _driveManager));
        }

        private void SecuritySettingsLblClick(object sender, EventArgs e)
        {
            new SecurityOptionsForm(_driveManager, Journal, this).ShowDialog();
        }

        public void SetEnabledOfJonControls(bool enabled)
        {
            Control[] controls = { securitySettingsLbl, continueBtn, passwordTxtBox };
            foreach (Control control in controls)
            {
                void SetEnabled() => control.Enabled = enabled;
                if (control.InvokeRequired)
                {
                    control.Invoke((Action)SetEnabled);
                }
                else
                {
                    SetEnabled();
                }
            }
        }

        private void ConnectAutomaticallyCheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.AutoConnect = connectAutomaticallyCheckBox.Checked;
        }


    }
}
