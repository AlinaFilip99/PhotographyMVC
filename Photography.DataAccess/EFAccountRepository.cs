using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFAccountRepository : IAccountRepository
    {
        private readonly PhotographyContext dbContext;
        public EFAccountRepository(PhotographyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Account> GetAll()
        {
            return dbContext.Accounts.AsEnumerable();
        } 
        public Account GetById(string id)
        {
            return dbContext.Accounts
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public Account Add(Account accountAdd)
        {
            var entity = dbContext.Accounts.Add(accountAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public Account Update (Account accountUpdate)
        {
            var entity = dbContext.Accounts.Update(accountUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public bool Remove(string id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Accounts.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(string id)
        {
            return dbContext.Accounts.Any(e => e.Id == id);
        }
        public IEnumerable<Account> GetByName(string searchString)
        {
            return dbContext.Accounts.Where(s => s.Nume.Contains(searchString)
                                       || s.Prenume.Contains(searchString));
        }
    }
}
