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
        public string userName { get; set; }
        public string nume { get; set; }
        public string prenume { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string twitter { get; set; }
    }
}
