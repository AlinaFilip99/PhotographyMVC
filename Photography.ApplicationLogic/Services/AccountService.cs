using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

namespace Photography.ApplicationLogic.Services
{
    public class AccountService
    {
        private readonly  IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accountRepository.GetAll();
        }
    }
}
