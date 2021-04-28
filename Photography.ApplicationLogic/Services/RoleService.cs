using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

namespace Photography.ApplicationLogic.Services
{
    public class RoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetRoles()
        {
            return roleRepository.GetAll();
        }
        public Role GetRoleById(int id)
        {
            return roleRepository.GetById(id);
        }
        public Role AddRole(Role roleToAdd)
        {
            return roleRepository.Add(roleToAdd);
        }
        public Role UpdateRole(Role roleToUpdate)
        {
            return roleRepository.Update(roleToUpdate);
        }
        public bool RemoveRole(int id)
        {
            return roleRepository.Remove(id);
        }
        public bool CheckRole(int id)
        {
            return roleRepository.Exists(id);
        }
    }
}
