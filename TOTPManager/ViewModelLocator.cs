using TOTPManager.Services.Accounts;
using TOTPManager.Services.Encryption;
using TOTPManager.Services.Windows;
using TOTPManager.ViewModels;

namespace TOTPManager
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            _textEncryption = new WindowTextEncryption();
            _accountService = new AccountService(_textEncryption);
            _windowFactory = new WindowFactory();
            _windowManager = new WindowManager(_windowFactory);
        }

        private ITextEncryption _textEncryption { get; }

        private IAccountService _accountService { get; }

        private IWindowFactory _windowFactory { get; }

        private IWindowManager _windowManager { get; }

        public MainViewModel MainViewModel => new MainViewModel(_accountService);

        public AccountsViewModel AccountsViewModel => new AccountsViewModel(_accountService, _windowManager);

        public SettingsViewModel SettingsViewModel => new SettingsViewModel();

        public NewAccountViewModel NewAccountViewModel => new NewAccountViewModel(_accountService);

    }
}
