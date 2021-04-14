using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public byte[] Picture { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
