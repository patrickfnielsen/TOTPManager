using TOTPManager.Services.Accounts;
using TOTPManager.Services.Encryption;
using TOTPManager.ViewModels;

namespace TOTPManager
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            _textEncryption = new WindowTextEncryption();
            _accountService = new AccountService(_textEncryption);
        }

        private ITextEncryption _textEncryption { get; }

        private IAccountService _accountService { get; }

        public MainViewModel MainViewModel => new MainViewModel(_accountService);

        public AccountsViewModel AccountsViewModel => new AccountsViewModel(_accountService);

        public SettingsViewModel SettingsViewModel => new SettingsViewModel();

        public NewAccountViewModel NewAccountViewModel => new NewAccountViewModel(_accountService);

    }
}
