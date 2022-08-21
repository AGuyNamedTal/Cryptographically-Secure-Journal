using System;
using System.Threading;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class NewJournalForm : Form
    {
        private readonly DriveManager _driveManager;
        private Tuple<int, string>[] _securityQuestions;
        public NewJournalForm(DriveManager driveManager)
        {
            InitializeComponent();
            _driveManager = driveManager;

        }

        private void CreateNewJournalBtnClick(object sender, EventArgs e)
        {
            if (!PasswordSelectionForm.IsValidPass(passTxtBox.Text, verPassTxtBox.Text))
            {
                return;
            }
            new Thread(GenerateJournal).Start();
        }

        private void GenerateJournal()
        {
            const int beforeUploadValue = 65;
            (Journal journal, byte[] password) = Journal.GenerateNewJournal("",
                passTxtBox.Text, _securityQuestions, value =>
                {
                    UpdateProgressBar((int)(value * beforeUploadValue / 100d));
                });
            UpdateProgressBar(beforeUploadValue);
            _driveManager.UploadJournal(journal, value =>
            {
                UpdateProgressBar((int)(beforeUploadValue + (value / 100d * (100 - beforeUploadValue))));
            }, progress =>
            {
                if (JournalEditorForm.MsgBoxForFailedUpload(progress))
                {
                    return;
                }

                Invoke(new Action(() =>
                {
                    this.SwitchForm(() => new JournalEditorForm("", password, journal, _driveManager));
                }));
            });

        }

        private void UpdateProgressBar(int newValue)
        {
            progressBar.Invoke(new Action(() => progressBar.Value = newValue));
        }


        private void SecQuestionsCheckedChanged(object sender, EventArgs e)
        {
            if (secQuestionsCheckbox.Checked)
            {
                SecurityQuestionsForm securityQuestionsForm = new SecurityQuestionsForm(
                    "Select security questions", false);
                if (securityQuestionsForm.ShowDialog() == DialogResult.OK && securityQuestionsForm.Result != null)
                {
                    _securityQuestions = securityQuestionsForm.Result;
                }
                else
                {
                    secQuestionsCheckbox.Checked = false;
                }
            }
            else
            {
                _securityQuestions = null;
            }
        }


        private void BackBtnClick(object sender, EventArgs e)
        {
            this.SwitchForm(new StartupForm());
        }
    }
}
