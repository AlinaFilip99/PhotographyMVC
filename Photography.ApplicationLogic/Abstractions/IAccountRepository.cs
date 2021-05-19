using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IAccountRepository 
    {
        IEnumerable<Account> GetAll();
        Account GetById(string id);
        Account Add(Account accountAdd);
        Account Update(Account accountUpdate);
        bool Remove(string id);
        bool Exists(string id);
        IEnumerable<Account> GetByName(string searchString);
    }
}
