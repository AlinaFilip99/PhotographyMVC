using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class Role : DataEntity
    {
        public string RoleType { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
