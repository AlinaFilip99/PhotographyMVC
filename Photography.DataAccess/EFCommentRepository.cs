using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFCommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public EFCommentRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<Comment> GetAll()
        {
            return dbContext.Comments.AsEnumerable();
        }
        public new Comment GetById(int id)
        {
            return dbContext.Comments
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new Comment Add(Comment commentAdd)
        {
            var entity = dbContext.Comments.Add(commentAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new Comment Update(Comment commentUpdate)
        {
            var entity = dbContext.Comments.Update(commentUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Comments.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(int id)
        {
            return dbContext.Comments.Any(e => e.Id == id);
        }
    }
}
