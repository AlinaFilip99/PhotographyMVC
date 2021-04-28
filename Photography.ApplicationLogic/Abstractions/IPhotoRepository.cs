using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        new IEnumerable<Photo> GetAll();
        new Photo GetById(int id);
        new Photo Add(Photo photoAdd);
        new Photo Update(Photo photoUpdate);
        new bool Remove(int id);
        bool Exists(int id);
        public IEnumerable<Photo> GetByPost(int id);
    }
}
