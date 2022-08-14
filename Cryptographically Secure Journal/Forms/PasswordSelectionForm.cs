using System;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class PasswordSelectionForm : Form
    {
        public string Password;
        public PasswordSelectionForm()
        {
            InitializeComponent();
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            if (!IsValidPass(pass1TxtBox.Text, pass2TxtBox.Text))
            {
                return;
            }
            Password = pass1TxtBox.Text;
            DialogResult = DialogResult.OK;
        }

        private void CancelBtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void PasswordSelectionFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
        public static bool IsValidPass(string pass, string samePass)
        {
            if (pass != samePass)
            {
                MessageBox.Show("Passwords don't match", "Invalid password data", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            const int minChars = 5;
            if (String.IsNullOrWhiteSpace(pass) || samePass.Length < minChars)
            {
                MessageBox.Show($"The password must be at least {minChars} characters long and not empty",
                    "Invalid password data", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


    }
}
