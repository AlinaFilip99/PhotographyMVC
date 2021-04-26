using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFContactFormRepository : BaseRepository<ContactForm>, IContactFormRepository
    {
        public EFContactFormRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
