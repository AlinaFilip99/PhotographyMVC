using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IPostRepository : IRepository<Post>
    {
        new IEnumerable<Post> GetAll();
        new Post GetById(int id);
        new Post Add(Post postAdd);
        new Post Update(Post postUpdate);
        new bool Remove(int id);
        bool Exists(int id);
        IEnumerable<Post> GetByUser(int id);
    }
}
