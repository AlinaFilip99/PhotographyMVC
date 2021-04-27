using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Photography.ApplicationLogic.Models;

namespace Photography.ViewModels
{
    public class PhotoViewModel
    {
        public Photo photo { get; set; }
        public IFormFile PictureFile { get; set; }
    }
}
