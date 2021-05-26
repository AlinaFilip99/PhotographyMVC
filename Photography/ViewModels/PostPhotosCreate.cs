using Microsoft.AspNetCore.Http;
using Photography.ApplicationLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ViewModels
{
    public class PostPhotosCreate
    {
        public string description { get; set; }
        public List<IFormFile> pictureFiles { get; set; }
    }
}
