using System;
using System.Collections.Generic;
using TOTPManager.Models;

namespace TOTPManager.Services.Accounts
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account GetById(Guid id);

        void Add(string displayName, byte[] secret);

        void Remove(Guid id);

        event EventHandler<AccountEventArgs> AccountAdded;
        event EventHandler<AccountEventArgs> AccountRemoved;
    }
}
