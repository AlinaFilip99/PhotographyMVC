using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFPhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public EFPhotoRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<Photo> GetAll()
        {
            return dbContext.Photos.AsEnumerable();
        }
        public new Photo GetById(int id)
        {
            return dbContext.Photos
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new Photo Add(Photo photoAdd)
        {
            var entity = dbContext.Photos.Add(photoAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new Photo Update(Photo photoUpdate)
        {
            var entity = dbContext.Photos.Update(photoUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.Photos.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(int id)
        {
            return dbContext.Photos.Any(e => e.Id == id);
        }
        public IEnumerable<Photo> GetByPost(int id)
        {
            return dbContext.Photos.Where(entity => entity.PostId.Equals(id)).AsEnumerable();
        }

        public IEnumerable<Photo> GetByPostIds(IEnumerable<int> postsIds)
        {
            return dbContext.Photos.Where(x => postsIds.Contains(x.PostId)).AsEnumerable();
        }
    }
}
