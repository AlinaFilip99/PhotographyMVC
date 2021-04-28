using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFAccountRepository :BaseRepository<Account>, IAccountRepository
    {
        public EFAccountRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }

        public new IEnumerable<Account> GetAll()
        {
            return dbContext.Accounts.AsEnumerable();
        } 
        public new Account GetById(int id)
        {
            return dbContext.Accounts
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new Account Add(Account accountAdd)
        {
            var entity = dbContext.Accounts.Add(accountAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new Account Update (Account accountUpdate)
        {
            var entity = dbContext.Accounts.Update(accountUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
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
        public bool Exists(int id)
        {
            return dbContext.Accounts.Any(e => e.Id == id);
        }
    }
}
