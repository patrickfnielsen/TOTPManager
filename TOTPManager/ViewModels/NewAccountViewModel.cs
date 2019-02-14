using System.Windows.Input;
using TOTPManager.Helpers;
using TOTPManager.Interfaces;
using TOTPManager.Models;
using TOTPManager.Services.Accounts;

namespace TOTPManager.ViewModels
{
    public class NewAccountViewModel : NotifyBase
    {
        private readonly IAccountService _accountService;

        public NewAccountViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            Digits = 6;
            Algorithm = Algorithm.SHA1;
        }

        public ICommand SaveAccount => new SimpleDelegateCommand((obj) =>
        {
            var bytesSecret = Base32.ToBytes(Secret);
            var account = new Account(AccountName, bytesSecret, Digits, Algorithm);

            _accountService.Add(account);
            CloseWindow(obj as IClosable);
        }, 
        () => AccountName?.Length > 0 && Secret?.Length > 0);

        private void CloseWindow(IClosable window)
        {
            window?.Close();
        }

        private string _accountName;
        public string AccountName
        {
            get =>_accountName;
            set => Set(ref _accountName, value);
        }

        private string _secret;
        public string Secret
        {
            get => _secret;
            set => Set(ref _secret, value);
        }

        private int _digits;
        public int Digits
        {
            get => _digits;
            set => Set(ref _digits, value);
        }


        private Algorithm _algorithm;
        public Algorithm Algorithm
        {
            get => _algorithm;
            set => Set(ref _algorithm, value);
        }
    }
}
