using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFPostRepository : BaseRepository<Post>, IPostRepository
    {
        public EFPostRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
