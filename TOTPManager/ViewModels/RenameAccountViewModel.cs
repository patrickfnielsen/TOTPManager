using System.Windows.Input;
using TOTPManager.Interfaces;
using TOTPManager.Models;
using TOTPManager.Services.Accounts;

namespace TOTPManager.ViewModels
{
    public class RenameAccountViewModel : NotifyBase
    {
        private readonly IAccountService _accountService;

        public RenameAccountViewModel(IAccountService accountService, Account account)
        {
            _accountService = accountService;
            Account = account;
        }

        public ICommand SaveAccount => new SimpleDelegateCommand((obj) =>
        {
            _accountService.PersistUpdates();

            CloseWindow(obj as IClosable);
        },
        () => Account.AccountName?.Length > 0);

        private void CloseWindow(IClosable window)
        {
            window?.Close();
        }

        private Account _account;
        public Account Account
        {
            get => _account;
            set => Set(ref _account, value);
        }
    }
}
