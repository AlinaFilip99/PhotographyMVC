using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photography.ApplicationLogic.Models
{
    public class ContactForm
    {
        public int ContactFormId { get; set; }
        public string NumeF { get; set; }
        public string PrenumeF { get; set; }
        public DateTime DataF { get; set; }
        public string Message { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
