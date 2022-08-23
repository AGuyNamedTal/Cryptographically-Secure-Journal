using System;
using System.Windows.Forms;

namespace CryptographicallySecureJournal.Utils
{
    public class ProgressUpdater
    {
        private readonly Action<double> _updateAction;

        public ProgressUpdater(ProgressBar progressBar)
        {
            void SetProgress(double value)
            {
                progressBar.Value = (int)Math.Round((progressBar.Maximum - progressBar.Minimum) * value);
            }


            _updateAction = val =>
            {
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke(new Action(() => SetProgress(val)));
                }
                else
                {
                    SetProgress(val);
                }
            };
        }

        public ProgressUpdater(ProgressUpdater parent, double initial, double end)
        {
            _updateAction = val =>
            {
                int newVal = (int)Math.Round(val * (end - initial));
                parent._updateAction(newVal);
            };
            Update(initial);
        }

        public void Update(double value)
        {
            _updateAction(value);
        }

    }
}
