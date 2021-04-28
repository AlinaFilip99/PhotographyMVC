using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IContactFormRepository : IRepository<ContactForm>
    {
        new IEnumerable<ContactForm> GetAll();
        new ContactForm GetById(int id);
        new ContactForm Add(ContactForm contactformAdd);
        new ContactForm Update(ContactForm contactformUpdate);
        new bool Remove(int id);
        bool Exists(int id);
    }
}
