using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFPhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public EFPhotoRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
    }
}
