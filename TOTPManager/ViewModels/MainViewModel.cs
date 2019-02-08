using Framework.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using TOTP.Services.TOTP;
using TOTPManager.Models;
using TOTPManager.Services.Accounts;

namespace TOTPManager.ViewModels
{
    public class MainViewModel : NotifyBase
    {
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };

        private IAccountService _accountService;
        private Dictionary<Guid, TOTPGenerator> _generators = new Dictionary<Guid, TOTPGenerator>();

        public MainViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _accountService.AccountAdded += AccountAdded;
            _accountService.AccountRemoved += AccountRemoved;

            Accounts = new ObservableCollection<Account>(_accountService.GetAll());

            UpdateTotpKeys();

            _dispatcherTimer.Tick += (sender, args) => UpdateTotpKeys();
            _dispatcherTimer.Start();
        }

        private void AccountRemoved(object sender, AccountEventArgs e)
        {
            Accounts.Remove(e.Account);
        }

        private void AccountAdded(object sender, AccountEventArgs e)
        {
            Accounts.Add(e.Account);
        }

        private void UpdateTotpKeys()
        {
            foreach(var account in Accounts)
            {
                var generator = GetKeyGeneratorForAccount(account);
                var key = generator.Generate();

                account.CurrentKey = FormatKeyForDisplay(key, account.Digits);
                account.PercentTimeUntilKeyExpire = (generator.TimeLeft / generator.Period) * 100;
                account.WillExpireSoon = (account.PercentTimeUntilKeyExpire < 20) ? true : false;
            }
        }

        private TOTPGenerator GetKeyGeneratorForAccount(Account account)
        {
            var containsGenerator = _generators.TryGetValue(account.Id, out var generator);
            if (!containsGenerator)
            {
                generator = new TOTPGenerator(account.Secret, account.Digits, 30, account.Algorithm);
                _generators.Add(account.Id, generator);
            }

            return generator;
        }

        private string FormatKeyForDisplay(int key, int digits)
        {
            var stringKey = key.ToString().PadLeft(digits, '0');
            var firstHalf = stringKey.Substring(0, stringKey.Length / 2);
            var secondHalf = stringKey.Substring(stringKey.Length / 2, stringKey.Length / 2);

            return $"{firstHalf} {secondHalf}";
        }

        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set => Set(ref _accounts, value);
        }
    }
}
