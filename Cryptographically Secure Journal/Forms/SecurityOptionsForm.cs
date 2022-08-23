using CryptographicallySecureJournal.Crypto;
using CryptographicallySecureJournal.Utils;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class SecurityOptionsForm : Form
    {
        private readonly DriveManager _driveManager;
        private Journal _journal;
        private readonly StartupForm _owner;
        private readonly ProgressUpdater _progressUpdater;
        public SecurityOptionsForm(DriveManager driveManager, Journal journal, StartupForm owner)
        {
            InitializeComponent();
            _driveManager = driveManager;
            _journal = journal;
            _owner = owner;
            _progressUpdater = new ProgressUpdater(progressBar);
        }

        private void ResetPassBtnClick(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            const double afterKeyVal = 0.75;
            new Thread(() =>
            {
                GetEncryptionKey(new ProgressUpdater(_progressUpdater, 0, afterKeyVal),
                    key =>
                    {
                        string oldText;
                        try
                        {
                            oldText = Encoding.UTF8.GetString(AesEncryption.Decrypt(_journal.EncryptedText, key));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message} - can't decrypt journal, possible wrong answers to security" +
                                            $"questions, wrong password or corrupt journal", "Can't Recover Journal",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _progressUpdater.Update(0);
                            return;
                        }

                        PasswordSelectionForm passForm = new PasswordSelectionForm();
                        if (passForm.ShowDialog() != DialogResult.OK || passForm.Password == null)
                        {
                            return;
                        }

                        string newPass = passForm.Password;

                        SecurityQuestionsForm securityQuestionsForm = new SecurityQuestionsForm(
                            "Please select new security questions",
                            true, _journal.QuestionsIndexes, false);
                        if (securityQuestionsForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        Tuple<int, string>[] newSecurityQuestions = securityQuestionsForm.Result;
                        ChangePassword(oldText, newPass, newSecurityQuestions, new ProgressUpdater(_progressUpdater, afterKeyVal, 1));
                    });
            })
            {
                ApartmentState = ApartmentState.STA
            }.Start();



        }

        private void GetEncryptionKey(ProgressUpdater progressUpdater, Action<byte[]> onComplete)
        {
            void GetByPassword()
            {
                string password = Interaction.InputBox("Security questions not found," +
                                                       "Please enter old password", "Enter Password");
                if (String.IsNullOrWhiteSpace(password))
                {
                    onComplete(null);
                }
                else
                {
                    byte[] key = HashAndSalt.Password(Encoding.UTF8.GetBytes(password),
                        _journal.PassSalt);
                    progressUpdater.Update(1);
                    onComplete(key);
                }
            }

            void GetBySecurityQuestions()
            {
                int[] questionsIndexes = _journal.QuestionsIndexes;
                SecurityQuestionsForm securityQuestionsForm = new SecurityQuestionsForm(
                    $"Answer {ShamirSecretSharing.MinNumOfShares} out of {ShamirSecretSharing.TotalShares}" +
                    $" security" +
                    $" questions (the 4th one will be ignored)", false, questionsIndexes, true);
                if (securityQuestionsForm.ShowDialog() != DialogResult.OK || securityQuestionsForm.Result == null)
                {
                    return;
                }

                Tuple<int, string>[] securityQuestions = securityQuestionsForm.Result;
                byte[] pass;
                try
                {
                    pass = EncryptedShare.RecoverKey(securityQuestions,
                        _journal.EncryptedShares, _progressUpdater);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message} - can't recover key, possible wrong answers to security" +
                                    $"questions", "Can't Recover Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _progressUpdater.Update(0);

                    onComplete(null);
                    return;
                }
                onComplete(pass);

            }

            if (_journal.EncryptedShares == null)
            {
                GetByPassword();
            }
            else
            {
                MethodOfRecoveryForm passForm = new MethodOfRecoveryForm();
                if (passForm.ShowDialog() != DialogResult.OK)
                {
                    onComplete(null);
                }
                switch (passForm.Result)
                {
                    case MethodOfRecoveryForm.RecoveryMethod.Password:
                        GetByPassword();
                        break;
                    case MethodOfRecoveryForm.RecoveryMethod.SecQuestions:
                        GetBySecurityQuestions();
                        break;
                }
            }
        }


        private void ChangePassword(string oldText, string newPass, Tuple<int, string>[] newSecurityQuestions,
           ProgressUpdater progressUpdater)
        {
            const double beforeUploadProgress = 0.50;
            progressUpdater.Update(0);
            (Journal journal, byte[] _) = Journal.GenerateNewJournal(oldText,
                newPass, newSecurityQuestions, new ProgressUpdater(progressUpdater, 0, beforeUploadProgress));
            UpdateJournal(journal, new ProgressUpdater(progressUpdater, beforeUploadProgress, 1));

        }

        private void UpdateJournal(Journal journal, ProgressUpdater progressUpdater)
        {
            _driveManager.UploadJournal(journal, progressUpdater, progress =>
           {
               if (JournalEditorForm.MsgBoxForFailedUpload(progress))
               {
                   return;
               }
               _journal = journal;
               MessageBox.Show("Changed successfully", "Success", MessageBoxButtons.OK);

           });
        }

        private void ResetSecQuestionsBtnClick(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            string password = Interaction.InputBox("Enter Password", "Password");
            if (password.Length == 0)
            {
                return;
            }

            int[] questionsIndexes = _journal.QuestionsIndexes;
            SecurityQuestionsForm securityQuestionsForm =
                new SecurityQuestionsForm("Please select new security questions",
                true, questionsIndexes, false);
            if (securityQuestionsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Tuple<int, string>[] newSecurityQuestions = securityQuestionsForm.Result;
            new Thread(() => ChangeSecurityQuestions(password, newSecurityQuestions)).Start();

        }

        private void ChangeSecurityQuestions(string password, Tuple<int, string>[] newSecurityQuestions)
        {
            byte[] key = HashAndSalt.Password(Encoding.UTF8.GetBytes(password), _journal.PassSalt);
            const double afterHashProgress = 0.25;
            _progressUpdater.Update(afterHashProgress);
            const double beforeUploadProgress = 0.75;
            string decryptedText;
            try
            {
                decryptedText = Encoding.UTF8.GetString(
                    AesEncryption.Decrypt(_journal.EncryptedText, key));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} - can't recover journal, probably wrong password or corrupt journal",
                    "Can't Recover Journal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            (Journal journal, byte[] _) =
                Journal.GenerateNewJournal(decryptedText, password, newSecurityQuestions,
                    new ProgressUpdater(_progressUpdater, afterHashProgress, beforeUploadProgress));
            UpdateJournal(journal,
                new ProgressUpdater(_progressUpdater, beforeUploadProgress, 1));

        }

        private void BackupJonLocallyBtnClick(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = DriveManager.FileName,
                AddExtension = false,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Choose where to save",
                RestoreDirectory = true
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string path = dialog.FileName;
                FileStream fileStream;
                try
                {
                    fileStream = File.Create(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Can't create/overwrite file - {ex.Message}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                MemoryStream memoryStream = _journal.ToMemoryStream();
                try
                {
                    fileStream.Write(memoryStream.ToArray());
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Can't write to file - {ex.Message}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    fileStream?.Dispose();
                    memoryStream?.Dispose();
                }
            }
        }

        private void BackupJournalDriveClick(object sender, EventArgs e)
        {
            string fileName = Interaction.InputBox("Choose file name", "Drive File Name");
            if (String.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
            _driveManager.UploadBackup(_journal, fileName, _progressUpdater, progress =>
            {
                if (JournalEditorForm.MsgBoxForFailedUpload(progress))
                {
                    return;
                }
                MessageBox.Show("Uploaded successfully", "Success", MessageBoxButtons.OK);
            });
        }

        private void DeleteJonBtnClick(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the journal from Google Drive?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            progressBar.Value = 0;
            _driveManager.DeleteJournal(task =>
            {
                if (task.Exception == null)
                {
                    _progressUpdater.Update(1);
                    _journal = null;
                    _owner.Journal = null;
                    _owner.SetEnabledOfJonControls(false);
                    _owner.ProgressBar.Update(0);
                    this.CloseOnUIThread();
                }
                else
                {
                    MessageBox.Show($"An error occurred - {task.Exception.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void SecurityOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            _owner.Journal = _journal;
        }
    }
}
