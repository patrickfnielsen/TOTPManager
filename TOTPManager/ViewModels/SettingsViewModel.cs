using Framework.View;
using System.Diagnostics;
using TOTPManager.Services.Startup;

namespace TOTPManager.ViewModels
{
    public class SettingsViewModel : NotifyBase
    {
        private readonly Startup _startup;

        public SettingsViewModel()
        {
            var process = Process.GetCurrentProcess();
            _startup = new Startup("TOTPManager", process.MainModule.FileName);
            RunOnStartup = _startup.IsInStartup();
        }

        private bool _runOnStartup;
        public bool RunOnStartup
        {
            get => _runOnStartup;
            set {
                ChangeStartupSetting(value);
                Set(ref _runOnStartup, value);
            }
        }

        private void ChangeStartupSetting(bool value)
        {
            if (value == true && !_startup.IsInStartup())
            {
                _startup.RunOnStartup();
            }

            if (value == false && _startup.IsInStartup())
            {
                _startup.RemoveFromStartup();
            }
        }
    }
}
