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
        public SecurityOptionsForm(DriveManager driveManager, Journal journal, StartupForm owner)
        {
            InitializeComponent();
            _driveManager = driveManager;
            _journal = journal;
            _owner = owner;
        }

        private void ResetPassBtnClick(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            const int afterKeyVal = 75;
            new Thread(() =>
            {
                GetEncryptionKey(value => { UpdateProgressBar((int)(value / 100d * afterKeyVal)); },
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
                            UpdateProgressBar(0);
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
                        ChangePassword(key, oldText, newPass, newSecurityQuestions,
                            value => { UpdateProgressBar((int)(value / 100d * (100 - afterKeyVal) + afterKeyVal)); });
                    });
            })
            {
                ApartmentState = ApartmentState.STA
            }.Start();



        }

        private void GetEncryptionKey(Action<int> updateProgress, Action<byte[]> onComplete)
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
                    updateProgress(100);
                    onComplete(key);
                }
            }

            void GetBySecurityQuestions()
            {
                int[] questionsIndexes = _journal.QuestionsIndexes;
                SecurityQuestionsForm securityQuestionsForm = new SecurityQuestionsForm(
                    $"Answer {EncryptedShare.MinimumNumberOfQuestions} out of 4 security" +
                    $" questions (the 4th one will be ignored)", false, questionsIndexes, true);
                if (securityQuestionsForm.ShowDialog() != DialogResult.OK || securityQuestionsForm.Result == null)
                {
                    return;
                }

                Tuple<int, string>[] securityQuestions = securityQuestionsForm.Result;
                byte[] pass;
                try
                {
                    pass = EncryptedShare.RecoverPassword(securityQuestions,
                        _journal.EncryptedShares, updateProgress);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message} - can't recover password, possible wrong answers to security" +
                                    $"questions", "Can't Recover Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    updateProgress(0);
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


        private void ChangePassword(byte[] key, string oldText, string newPass, Tuple<int, string>[] newSecurityQuestions,
            Action<int> progress)
        {


            const int beforeUploadProgress = 50;
            (Journal journal, byte[] _) = Journal.GenerateNewJournal(oldText,
                newPass, newSecurityQuestions, val =>
                { progress((int)(beforeUploadProgress / 100d * val)); });
            UpdateJournal(journal, val =>
            {
                UpdateProgressBar((int)(val / 100d * (100 - beforeUploadProgress) + beforeUploadProgress));
            });

        }

        private void UpdateJournal(Journal journal, Action<int> updateProgressBar)
        {
            _driveManager.UploadJournal(journal, updateProgressBar, progress =>
           {
               if (JournalEditorForm.MsgBoxForFailedUpload(progress))
               {
                   return;
               }
               _journal = journal;
               MessageBox.Show("Changed successfully", "Success", MessageBoxButtons.OK);

           });
        }
        private void UpdateProgressBar(int val)
        {
            progressBar.Invoke(new Action(() => progressBar.Value = val));
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
            byte[] hashedPass = HashAndSalt.Password(Encoding.UTF8.GetBytes(password), _journal.PassSalt);
            const int afterHashProgress = 25;
            const int beforeUploadProgress = 75;
            UpdateProgressBar(afterHashProgress);
            string decryptedText;
            try
            {
                decryptedText = Encoding.UTF8.GetString(
                    AesEncryption.Decrypt(_journal.EncryptedText, hashedPass));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} - can't recover journal, probably wrong password or corrupt journal",
                    "Can't Recover Journal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            (Journal journal, byte[] _) =
                Journal.GenerateNewJournal(decryptedText, password, newSecurityQuestions,
                    value =>
                    {
                        UpdateProgressBar((int)((value / 100d) * (beforeUploadProgress - afterHashProgress) + afterHashProgress));
                    });
            UpdateProgressBar(beforeUploadProgress);
            UpdateJournal(journal, value =>
            {
                UpdateProgressBar((int)(value / 100d * (100 - beforeUploadProgress) + beforeUploadProgress));
            });

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
            _driveManager.UploadBackup(_journal, fileName, UpdateProgressBar, progress =>
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
                    UpdateProgressBar(100);
                    _journal = null;
                    _owner.Journal = null;
                    _owner.SetEnabledOfJonControls(false);
                    _owner.UpdateProgressBar(0);
                    this.CloseOnUIThread();
                }
                else
                {
                    MessageBox.Show($"An error occurred - {task.Exception.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
    }
}
