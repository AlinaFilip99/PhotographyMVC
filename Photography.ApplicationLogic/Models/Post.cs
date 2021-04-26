using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Post : DataEntity
    {
        public string Description { get; set; }
        public int Likes { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
