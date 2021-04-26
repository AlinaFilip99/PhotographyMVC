using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class BaseRepository<T> : IRepository<T> where T : DataEntity
    {
        protected readonly PhotographyContext dbContext;
        public BaseRepository(PhotographyContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public T Add(T itemToAdd)
        {
            var entity = dbContext.Add<T>(itemToAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }


        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                            .AsEnumerable();
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>()
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();

        }

        public bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Remove<T>(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbContext.Set<T>()
                                  .Update(itemToUpdate);
            //dbContext.Entry(itemToUpdate).State = EntityState.Modified;
            dbContext.SaveChanges();          
            return entity.Entity;
        }
    }
}
