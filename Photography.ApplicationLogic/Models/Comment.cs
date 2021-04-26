using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Comment: DataEntity
    {
        public string CommMessage { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
