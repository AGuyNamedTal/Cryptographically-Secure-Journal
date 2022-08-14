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
        public SecurityOptionsForm(DriveManager driveManager, Journal journal)
        {
            InitializeComponent();
            _driveManager = driveManager;
            _journal = journal;
        }

        private void ResetPassBtnClick(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            if (_journal.EncryptedShares == null)
            {
                MessageBox.Show("No security questions, can't reset password", "No Security Questions",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PasswordSelectionForm passForm = new PasswordSelectionForm();
            if (passForm.ShowDialog() != DialogResult.OK || passForm.Password == null)
            {
                return;
            }

            string newPass = passForm.Password;

            int[] questionsIndexes = _journal.QuestionsIndexes;
            SecurityQuestionsForm securityQuestionsForm = new SecurityQuestionsForm(
                $"Answer {EncryptedShare.MinimumNumberOfQuestions} out of 4 security" +
                $" questions (the 4th one will be ignored)", false, questionsIndexes, true);
            if (securityQuestionsForm.ShowDialog() != DialogResult.OK || securityQuestionsForm.Result == null)
            {
                return;
            }

            Tuple<int, string>[] oldSecurityQuestions = securityQuestionsForm.Result;

            securityQuestionsForm = new SecurityQuestionsForm("Please select new security questions",
                true, questionsIndexes, false);
            if (securityQuestionsForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Tuple<int, string>[] newSecurityQuestions = securityQuestionsForm.Result;


            new Thread(() =>
                ChangePassword(oldSecurityQuestions, newPass, newSecurityQuestions)).Start();
        }

        private void ChangePassword(Tuple<int, string>[] questionsAnswers, string newPass, Tuple<int, string>[] newSecurityQuestions)
        {

            const int afterAssemblyProgress = 50;
            byte[] oldPass;
            try
            {
                oldPass = EncryptedShare.RecoverPassword(questionsAnswers,
                    _journal.EncryptedShares, value =>
                    {
                        UpdateProgressBar((int)(value * afterAssemblyProgress / 100d));
                    });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} - can't recover password, possible wrong answers to security" +
                                $"questions", "Can't Recover Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateProgressBar(0);
                return;
            }
            const int beforeUploadProgress = 75;
            string oldText;
            try
            {
                oldText = Encoding.UTF8.GetString(AesEncryption.Decrypt(_journal.EncryptedText, oldPass));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} - can't decrypt journal, possible wrong answers to security" +
                                $"questions or corrupt journal", "Can't Recover Journal",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateProgressBar(0);
                return;
            }

            (Journal journal, byte[] _) = Journal.GenerateNewJournal(oldText,
                newPass, newSecurityQuestions, val =>
                {
                    int newVal = (int)(val / 100d * (beforeUploadProgress - afterAssemblyProgress) + afterAssemblyProgress);
                    UpdateProgressBar(newVal);
                });
            UpdateJournal(journal, beforeUploadProgress, 100);

        }

        private void UpdateJournal(Journal journal, int beforeUploadProgress, int afterUploadProgress)
        {
            _driveManager.UploadJournal(journal, value =>
            {
                UpdateProgressBar((int)(beforeUploadProgress + (value / 100d * (afterUploadProgress - beforeUploadProgress))));
            }, progress =>
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
            string decryptedText = Encoding.UTF8.GetString(
                AesEncryption.Decrypt(_journal.EncryptedText, hashedPass));
            (Journal journal, byte[] _) =
                Journal.GenerateNewJournal(decryptedText, password, newSecurityQuestions,
                    value =>
                    {
                        UpdateProgressBar((int)((value / 100d) * (beforeUploadProgress - afterHashProgress) + afterHashProgress));
                    });
            UpdateProgressBar(beforeUploadProgress);
            UpdateJournal(journal, beforeUploadProgress, 100);

        }

        private void BackupJonLocallyBtnClick(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = DriveManager.FileName,
                AddExtension = false,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
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

            if (MessageBox.Show("Are you sure you want to delete the journal from google drive?",
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
