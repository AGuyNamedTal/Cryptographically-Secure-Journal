using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace CryptographicallySecureJournal
{
    internal static class FormUtils
    {
        public static void ScrollToBottom(this TextBox textBox)
        {
            SendMessage(textBox.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);

        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;

        public static void SwitchForm(this Form oldForm, Form newForm)
        {
            Thread myThread = new Thread((ThreadStart)delegate
            {
                Application.Run(newForm);
            });
            myThread.SetApartmentState(ApartmentState.STA);
            myThread.Start();
            oldForm.Invoke(new Action(oldForm.Close));
        }
        public static void SwitchForm(this Form oldForm, Func<Form> newForm)
        {
            Thread myThread = new Thread((ThreadStart)delegate
            {
                Application.Run(newForm());
            });
            myThread.SetApartmentState(ApartmentState.STA);
            myThread.Start();
            oldForm.Invoke(new Action(oldForm.Close));
        }

        public static void CloseOnUIThread(this Form form)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(form.Close));
            }
            else
            {
                form.Close();
            }
        }
    }
}
