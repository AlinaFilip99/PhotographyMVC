using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Photography.ApplicationLogic.Models;

namespace Photography.ViewModels
{
    public class PostPhotosCommentsViewModel
    {
        public Post post { get; set; }
        public IEnumerable<Photo> photos { get; set; }
        public IEnumerable<Comment> comments { get; set; }
    }
}
