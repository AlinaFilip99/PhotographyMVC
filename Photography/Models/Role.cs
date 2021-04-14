using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleType { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
