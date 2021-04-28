using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;

namespace Photography.ApplicationLogic.Services
{
    public class ContactFormService
    {
        private readonly IContactFormRepository contactRepository;

        public ContactFormService(IContactFormRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public IEnumerable<ContactForm> GetContactForms()
        {
            return contactRepository.GetAll();
        }
        public ContactForm GetContactFormById(int id)
        {
            return contactRepository.GetById(id);
        }
        public ContactForm AddContactForm(ContactForm contactToAdd)
        {
            return contactRepository.Add(contactToAdd);
        }
        public ContactForm UpdateContactForm(ContactForm contactToUpdate)
        {
            return contactRepository.Update(contactToUpdate);
        }
        public bool RemoveContactForm(int id)
        {
            return contactRepository.Remove(id);
        }
        public bool CheckContactForm(int id)
        {
            return contactRepository.Exists(id);
        }
        public IEnumerable<ContactForm> GetContactsByName(string searchString)
        {
            return contactRepository.GetByName(searchString);
        }
    }
}
