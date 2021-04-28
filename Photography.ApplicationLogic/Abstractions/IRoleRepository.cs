using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IRoleRepository : IRepository<Role>
    {
        new IEnumerable<Role> GetAll();
        new Role GetById(int id);
        new Role Add(Role roleAdd);
        new Role Update(Role roleUpdate);
        new bool Remove(int id);
        bool Exists(int id);
    }
}
