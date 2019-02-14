using System.Diagnostics;
using System.Windows.Input;
using TOTPManager.Models;
using TOTPManager.Services.Settings;
using TOTPManager.Services.Startup;

namespace TOTPManager.ViewModels
{
    public class SettingsViewModel : NotifyBase
    {
        private readonly Startup _startup;
        private readonly ISettings _settings;
        private readonly AppSettings _appSettings;

        public SettingsViewModel(ISettings settings)
        {
            _settings = settings;
            _appSettings = _settings.Load();
            SettingsChanged = false;

            var process = Process.GetCurrentProcess();
            _startup = new Startup("TOTPManager", process.MainModule.FileName);
            RunOnStartup = _startup.IsInStartup();
        }

        public ICommand SaveSettings => new SimpleDelegateCommand(obj =>
        {
            _appSettings.RunOnStartup = RunOnStartup;
            ChangeStartupSetting(RunOnStartup);

            _settings.Save(_appSettings);

            SettingsChanged = false;
        },
        () => SettingsChanged);

        private bool _settingsChanged;
        public bool SettingsChanged
        {
            get => _settingsChanged;
            set => Set(ref _settingsChanged, value);
        }

        private bool _runOnStartup;
        public bool RunOnStartup
        {
            get => _runOnStartup;
            set
            {
                SettingsChanged = _appSettings.RunOnStartup != value;
                Set(ref _runOnStartup, value);
            }
        }

        private void ChangeStartupSetting(bool value)
        {
            if (value && !_startup.IsInStartup())
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
