﻿using System;
using TOTPManager.Models;

namespace TOTPManager.Services.Accounts
{
    public class AccountEventArgs : EventArgs
    {
        public Account Account { get; }

        public AccountEventArgs(Account account)
        {
            Account = account;
        }
    }
}
