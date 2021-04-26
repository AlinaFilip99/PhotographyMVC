using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<ContactForm> ContactForms { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

}
