using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IAccountRepository : IRepository<Account>
    {
        new IEnumerable<Account> GetAll();
        new Account GetById(int id);
        new Account Add(Account accountAdd);
        new Account Update(Account accountUpdate);
        new bool Remove(int id);
        bool Exists(int id);
        IEnumerable<Account> GetByName(string searchString);
    }
}
