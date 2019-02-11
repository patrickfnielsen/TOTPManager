using System;
using System.Collections.Generic;
using System.Linq;
using TOTPManager.Models;
using Newtonsoft.Json;
using System.IO;
using TOTPManager.Helpers;
using TOTPManager.Services.Encryption;

namespace TOTPManager.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ITextEncryption _textEncryption;
        private string persistPath => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TOTPManager")}";
        private const string persistFile = "accounts.db";
        private readonly List<Account> _accounts = new List<Account>();

        public event EventHandler<AccountEventArgs> AccountAdded;
        public event EventHandler<AccountEventArgs> AccountRemoved;

        public AccountService(ITextEncryption textEncryption)
        {
            _textEncryption = textEncryption;
            LoadData();
        }

        public void Add(string displayName, byte[] secret)
        {
            var account = new Account(displayName, secret);
            _accounts.Add(account);
            PersistData();

            AccountAdded?.Invoke(this, new AccountEventArgs(account));
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

        public Account GetById(Guid Id)
        {
            var account = _accounts.Single(x => x.Id == Id);

            return account;
        }

        public void Remove(Guid Id)
        {
            var account = _accounts.Single(x => x.Id == Id);

            _accounts.Remove(account);

            PersistData();

            AccountRemoved?.Invoke(this, new AccountEventArgs(account));

        }

        private void PersistData()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new TypeOnlyContractResolver<Account>()
            };
            var jsonData = JsonConvert.SerializeObject(_accounts, jsonSettings);

            Directory.CreateDirectory(persistPath);

            var protectedData = _textEncryption.Encrypt(jsonData);
            File.WriteAllBytes(persistPath + "\\" + persistFile, protectedData);
        }

        private void LoadData()
        {
            if (!File.Exists(persistPath + "\\" + persistFile)) return;

            var protectedData = File.ReadAllBytes(persistPath + "\\" + persistFile);
            var jsonData = _textEncryption.Decrypt(protectedData);

            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(jsonData);

            foreach(var account in accounts)
            {
                _accounts.Add(account);
            }
        }
    }
}
