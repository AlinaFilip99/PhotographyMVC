using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IRepository<T> where T : DataEntity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        T Add(T entity);

        T Update(T entity);

        bool Remove(int id);
    }
}
