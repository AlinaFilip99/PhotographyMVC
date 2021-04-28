using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFPostRepository : BaseRepository<Post>, IPostRepository
    {
        public EFPostRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<Post> GetAll()
        {
            return dbContext.Posts.AsEnumerable();
        }
        public new Post GetById(int id)
        {
            return dbContext.Posts
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new Post Add(Post postAdd)
        {
            var entity = dbContext.Posts.Add(postAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new Post Update(Post postUpdate)
        {
            var entity = dbContext.Posts.Update(postUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Posts.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(int id)
        {
            return dbContext.Posts.Any(e => e.Id == id);
        }
        public IEnumerable<Post> GetByUser(int id)
        {
            return dbContext.Posts.Where(entity => entity.AccountId.Equals(id)).AsEnumerable();
        }
    }
}
