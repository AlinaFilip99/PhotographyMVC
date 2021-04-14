using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommMessage { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
