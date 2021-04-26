using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFAccountRepository :BaseRepository<Account>, IAccountRepository
    {
        public EFAccountRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
