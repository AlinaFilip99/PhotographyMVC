using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
