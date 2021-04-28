using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface ICommentRepository : IRepository<Comment>
    {
        new IEnumerable<Comment> GetAll();
        new Comment GetById(int id);
        new Comment Add(Comment commentAdd);
        new Comment Update(Comment commentUpdate);
        new bool Remove(int id);
        bool Exists(int id);
        public IEnumerable<Comment> GetByPost(int id);
    }
}
