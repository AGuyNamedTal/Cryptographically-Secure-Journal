using System.Windows.Forms;

namespace CryptographicallySecureJournal.Utils
{
    public class UpdateableProgressBar : ProgressBar
    {
        public ProgressUpdater Updater;
        public UpdateableProgressBar()
        {
            Updater = new ProgressUpdater(this);
        }

        public void Update(double val)
        {
            Updater.Update(val);
        }
    }
}
