namespace TOTPManager.Models
{
    public class AppSettings : NotifyBase
    {
        public AppSettings()
        {
            RunOnStartup = true;
        }

        private bool _runOnStartup;
        public bool RunOnStartup
        {
            get => _runOnStartup;
            set => Set(ref _runOnStartup, value);
        }
    }
}
