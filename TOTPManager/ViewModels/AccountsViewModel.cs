using System.Collections.ObjectModel;
using System.Windows.Input;
using TOTPManager.Models;
using TOTPManager.Services.Accounts;
using TOTPManager.Services.Windows;

namespace TOTPManager.ViewModels
{
    public class AccountsViewModel : NotifyBase
    {
        private readonly IAccountService _accountService;
        private readonly IWindowManager _windowManager;

        public AccountsViewModel(IAccountService accountService, IWindowManager windowManager)
        {
            _accountService = accountService;
            _windowManager = windowManager;

            Accounts = new ObservableCollection<Account>(_accountService.GetAll());

            _accountService.AccountAdded += AccountAdded;
            _accountService.AccountRemoved += AccountRemoved;
        }

        private void AccountRemoved(object sender, AccountEventArgs e)
        {
            Accounts.Remove(e.Account);
        }

        private void AccountAdded(object sender, AccountEventArgs e)
        {
            Accounts.Add(e.Account);
        }

        public ICommand NewAccount => new SimpleDelegateCommand((obj) =>
        {
            _windowManager.ShowDialog(new NewAccountViewModel(_accountService));
        });

        public ICommand EditAccount => new SimpleDelegateCommand((obj) =>
        {
            _windowManager.ShowDialog(new RenameAccountViewModel(_accountService, SelectedAccount));
        }, () => SelectedAccount != null);

        public ICommand DeleteAccount => new SimpleDelegateCommand((obj) =>
        {
            _accountService.Remove(SelectedAccount.Id);
        }, () => SelectedAccount != null);


        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set => Set(ref _selectedAccount, value);
        }

        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set => Set(ref _accounts, value);
        }
    }
}
