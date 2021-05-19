using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFRoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public EFRoleRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }/*
        public new IEnumerable<Role> GetAll()
        {
            return dbContext.Roles.AsEnumerable();
        }
        public new Role GetById(int id)
        {
            return dbContext.Roles
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new Role Add(Role roleAdd)
        {
            var entity = dbContext.Roles.Add(roleAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new Role Update(Role roleUpdate)
        {
            var entity = dbContext.Roles.Update(roleUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Roles.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(int id)
        {
            return dbContext.Roles.Any(e => e.Id == id);
        }*/
    }
}
