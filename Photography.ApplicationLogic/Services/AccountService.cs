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
        public Account GetAccountById(int id)
        {
            return accountRepository.GetById(id);
        }
        public Account AddAccount(Account accountToAdd)
        {
            return accountRepository.Add(accountToAdd);
        }
        public Account UpdateAccount(Account accountToUpdate)
        {
            return accountRepository.Update(accountToUpdate);
        }
        public bool RemoveAccount(int id)
        {
            return accountRepository.Remove(id);
        }
        public bool CheckAccount(int id)
        {
            return accountRepository.Exists(id);
        }
        public IEnumerable<Account> GetAccountsByName(string searchString)
        {
            return accountRepository.GetByName(searchString);
        }
    }
}
