using Photography.ApplicationLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ViewModels
{
    public class AccountPhotos
    {
        public Account account { get; set; }
        public IEnumerable<Photo> photos { get; set; }
    }
}
