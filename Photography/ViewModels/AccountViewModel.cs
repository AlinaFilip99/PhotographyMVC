using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Photography.ApplicationLogic.Models;

namespace Photography.ViewModels
{
    public class AccountViewModel
    {
        public Account account { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
    }
}
