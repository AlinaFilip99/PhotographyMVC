using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Photo : DataEntity
    {
        public byte[] Picture { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
