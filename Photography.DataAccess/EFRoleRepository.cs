using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFRoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public EFRoleRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
