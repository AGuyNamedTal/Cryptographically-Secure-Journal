using System;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class MethodOfRecoveryForm : Form
    {
        public enum RecoveryMethod
        {
            Password,
            SecQuestions
        }

        public RecoveryMethod Result;

        public MethodOfRecoveryForm()
        {
            InitializeComponent();
        }

        private void PasswordRecBtnClick(object sender, EventArgs e)
        {
            Result = RecoveryMethod.Password;
            DialogResult = DialogResult.OK;
        }

        private void BySecQuestionBtnClick(object sender, EventArgs e)
        {
            Result = RecoveryMethod.SecQuestions;
            DialogResult = DialogResult.OK;
        }

        private void CancelBtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void MethodOfRecoveryFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
