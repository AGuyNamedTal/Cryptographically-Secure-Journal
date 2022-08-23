using CryptographicallySecureJournal.Utils;
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
            const double beforeUploadValue = 0.65;
            (Journal journal, byte[] key) = Journal.GenerateNewJournal("",
                passTxtBox.Text, _securityQuestions, new ProgressUpdater(progressBar.Updater,
                    0, beforeUploadValue));
            _driveManager.UploadJournal(journal, new ProgressUpdater(progressBar.Updater,
                beforeUploadValue, 1d), progress =>
            {
                if (JournalEditorForm.MsgBoxForFailedUpload(progress))
                {
                    return;
                }

                Invoke(new Action(() =>
                {
                    this.SwitchForm(() => new JournalEditorForm("", key, journal, _driveManager));
                }));
            });

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
