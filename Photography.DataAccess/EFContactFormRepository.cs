using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Photography.ApplicationLogic.Abstractions;
using Photography.ApplicationLogic.Models;

namespace Photography.DataAccess
{
    public class EFContactFormRepository : BaseRepository<ContactForm>, IContactFormRepository
    {
        public EFContactFormRepository(PhotographyContext dbContext) : base(dbContext)
        {

        }
        public new IEnumerable<ContactForm> GetAll()
        {
            return dbContext.ContactForms.AsEnumerable();
        }
        public new ContactForm GetById(int id)
        {
            return dbContext.ContactForms
                            .Where(entity => entity.Id.Equals(id))
                            .SingleOrDefault();
        }
        public new ContactForm Add(ContactForm contactformAdd)
        {
            var entity = dbContext.ContactForms.Add(contactformAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new ContactForm Update(ContactForm contactformUpdate)
        {
            var entity = dbContext.ContactForms.Update(contactformUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }
        public new bool Remove(int id)
        {
            var entityToRemove = GetById(id);
            if (entityToRemove != null)
            {
                dbContext.ContactForms.Remove(entityToRemove);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exists(int id)
        {
            return dbContext.ContactForms.Any(e => e.Id == id);
        }
        public IEnumerable<ContactForm> GetByName (string searchString)
        {
            return dbContext.ContactForms.Where(s => s.NumeF.Contains(searchString)
                                       || s.PrenumeF.Contains(searchString)).AsEnumerable();
        }
    }
}
