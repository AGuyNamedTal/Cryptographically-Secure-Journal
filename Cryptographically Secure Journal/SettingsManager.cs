using CryptographicallySecureJournal.Properties;

namespace CryptographicallySecureJournal
{
    public static class SettingsManager
    {
        private static readonly Settings Settings = Settings.Default;

        public static bool AutoConnect
        {
            get => Settings.AUTO_CONNECT;
            set
            {
                Settings.AUTO_CONNECT = value;
                Settings.Save();
            }
        }
    }
}
