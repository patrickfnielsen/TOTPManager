using System;
using System.Collections.Generic;
using System.Linq;
using TOTPManager.Models;
using Newtonsoft.Json;
using System.IO;
using TOTPManager.Helpers;
using TOTPManager.Services.Encryption;
using TOTPManager.Services.Storage;

namespace TOTPManager.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly ITextEncryption _textEncryption;
        private readonly IFile _file;
        private readonly IDirectory _directory;

        private string persistPath => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TOTPManager")}";
        private const string persistFile = "accounts.db";
        private readonly List<Account> _accounts = new List<Account>();

        public event EventHandler<AccountEventArgs> AccountAdded;
        public event EventHandler<AccountEventArgs> AccountRemoved;

        public AccountService(ITextEncryption textEncryption, IFile file, IDirectory directory)
        {
            _textEncryption = textEncryption;
            _file = file;
            _directory = directory;

            LoadData();
        }

        public void Add(Account account)
        {
            _accounts.Add(account);
            PersistData();

            AccountAdded?.Invoke(this, new AccountEventArgs(account));
        }

        public void PersistUpdates()
        {
            PersistData();
        }

        public IEnumerable<Account> GetAll()
        {
            return _accounts;
        }

        public Account GetById(Guid id)
        {
            var account = _accounts.Single(x => x.Id == id);

            return account;
        }

        public void Remove(Guid id)
        {
            var account = _accounts.Single(x => x.Id == id);

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

            _directory.CreateDirectory(persistPath);

            var protectedData = _textEncryption.Encrypt(jsonData);
            _file.WriteAllBytes(persistPath + "\\" + persistFile, protectedData);
        }

        private void LoadData()
        {
            if (!_file.Exists(persistPath + "\\" + persistFile)) return;

            var protectedData = _file.ReadAllBytes(persistPath + "\\" + persistFile);
            var jsonData = _textEncryption.Decrypt(protectedData);

            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(jsonData);

            foreach(var account in accounts)
            {
                _accounts.Add(account);
            }
        }
    }
}
