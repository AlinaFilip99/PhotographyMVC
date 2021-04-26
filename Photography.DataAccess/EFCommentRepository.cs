using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFCommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public EFCommentRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
