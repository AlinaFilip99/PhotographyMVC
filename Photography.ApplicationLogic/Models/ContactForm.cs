using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class ContactForm : DataEntity
    {
        [DisplayName("Last Name")]
        public string NumeF { get; set; }
        [DisplayName("First Name")]
        public string PrenumeF { get; set; }
        [DisplayName("Date")]
        public DateTime DataF { get; set; }
        public string Message { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
