using CryptographicallySecureJournal.Crypto;
using CryptographicallySecureJournal.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Forms
{
    public partial class SecurityQuestionsForm : Form
    {

        public Tuple<int, string>[] Result;

        private readonly TextBox[] _secAnswers;
        private readonly ComboBox[] _secQuestions;
        private readonly int _requiredAnswers;


        public SecurityQuestionsForm(string titleTxt, bool showCheckBox)
        {
            InitializeComponent();
            _secAnswers = new[] { secAns1, secAns2, secAns3, secAns4 };
            _secQuestions = new[] { secQues1, secQues2, secQues3, secQues4 };



            _requiredAnswers = _secAnswers.Length;
            string[] securityQuestions = Resources.SecurityQuestions.Split('\n')
            .Select(s => s.Replace("\r", "")).ToArray();
            foreach (ComboBox comboBox in _secQuestions)
            {
                comboBox.Items.Clear();
                comboBox.Items.AddRange(securityQuestions);
            }

            titleLbl.Text = titleTxt;
            secQuestionsCheckbox.Visible = showCheckBox;
            //TODO: REMOVE THIS!!!!
            for (int i = 0; i < 4; i++)
            {
                _secQuestions[i].SelectedIndex = i;
                _secAnswers[i].Text = new string((char)('0' + i), 3);
            }
        }
        public SecurityQuestionsForm(string titleTxt, bool showCheckBox, int[] questions,
            bool requireAnswersOnly) : this(titleTxt, showCheckBox)
        {
            if (questions != null)
            {
                for (int i = 0; i < questions.Length; i++)
                {
                    _secQuestions[i].SelectedIndex = questions[i];
                }
            }

            if (requireAnswersOnly)
            {
                foreach (ComboBox secQuestion in _secQuestions)
                {
                    secQuestion.Enabled = false;
                }

                _requiredAnswers = ShamirSecretSharing.MinNumOfShares;
            }

        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            if (secQuestionsCheckbox.Checked)
            {
                if (!ValidateData())
                {
                    return;
                }

                Result = _secQuestions.Where((box, i) => !String.IsNullOrWhiteSpace(_secAnswers[i].Text))
                    .Select((question, i) =>
                    new Tuple<int, string>(question.SelectedIndex, _secAnswers[i].Text)).ToArray();
            }
            else
            {
                Result = null;
            }

            DialogResult = DialogResult.OK;
        }

        private bool ValidateData()
        {
            if (_secQuestions.Count(secQuestion => secQuestion.SelectedIndex != -1) < _requiredAnswers)
            {
                ShowInvalidDataMsgBox($"You must select {_requiredAnswers} questions");
                return false;
            }

            if (_secQuestions.Where(box => box.SelectedIndex != -1).
                    Select(box => box.SelectedIndex).Distinct().Count() != _secQuestions.Length)
            {
                ShowInvalidDataMsgBox("Each question must be unique");
                return false;

            }

            const int minChars = 3;
            for (int i = 0; i < _secQuestions.Length; i++)
            {
                if (_secQuestions[i].SelectedIndex != -1)
                {
                    string answer = _secAnswers[i].Text;
                    if (string.IsNullOrWhiteSpace(answer) || answer.Length < minChars)
                    {
                        ShowInvalidDataMsgBox($"Each answer must be at least {minChars} characters long and not empty");
                    }
                }
            }


            return true;
        }

        private static void ShowInvalidDataMsgBox(string text)
        {
            MessageBox.Show(text, "Invalid questions/answers", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CancelBtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SecurityQuestionsFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void SecQuestionsCheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in _secAnswers.Concat<Control>(_secQuestions))
            {
                control.Enabled = secQuestionsCheckbox.Checked;
            }
        }
    }
}
