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
            return dbContext.Users.AsEnumerable();
        } 
        public Account GetById(string id)
        {
            return dbContext.Users
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public Account Add(Account accountAdd)
        {
            var entity = dbContext.Users.Add(accountAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public Account Update (Account accountUpdate)
        {
            var entity = dbContext.Users.Update(accountUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public bool Remove(string id)
        {
            Account entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Users.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(string id)
        {
            return dbContext.Users.Any(e => e.Id == id);
        }
        public IEnumerable<Account> GetByName(string searchString)
        {
            return dbContext.Users.Where(s => s.Nume.Contains(searchString)
                                       || s.Prenume.Contains(searchString));
        }
    }
}
