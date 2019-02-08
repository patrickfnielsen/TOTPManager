using Framework.View;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TOTPManager.Models;
using TOTPManager.Services.Accounts;
using TOTPManager.Views;

namespace TOTPManager.ViewModels
{
    public class AccountsViewModel : NotifyBase
    {
        private readonly IAccountService _accountService;

        public AccountsViewModel(IAccountService accountService)
        {
            _accountService = accountService;

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
            //ToDo: Refactor
            var window = new NewAccountWindow();
            window.ShowDialog();
        });

        public ICommand EditAccount => new SimpleDelegateCommand((obj) =>
        {

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
