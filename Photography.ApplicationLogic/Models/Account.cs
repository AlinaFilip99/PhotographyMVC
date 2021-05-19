using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Account: IdentityUser
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string ProfilePicture { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public ICollection<ContactForm> ContactForms { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

}
