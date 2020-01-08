using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WEB.Interfaces;

namespace WEB.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationContext _contactcontext;

        public ContactService(ApplicationContext applicationContext)
        {
            _contactcontext = applicationContext;
        }

        public IQueryable<Contact> GetContacts()
        {
            return _contactcontext.contacts;
        }
        public Contact GetContactId(int id)
        {
            return GetContacts().Single(x => x.ContactId == id);
        }
        public IQueryable<Contact> GetContactsId(string id)
        {
            return GetContacts().Where(x => x.UserId == id);
        }
        public Contact GetContactsName(string Name)
        {
            var contact = _contactcontext.contacts.FirstOrDefault(x => x.ContactName == Name);
            return contact;
        }
        public Contact CreateContact(Contact contact)
        {
            _contactcontext.contacts.Add(contact);
            _contactcontext.SaveChanges();

            return contact;
        }

        public int GetAmountByUserId(string id)
        {
            return GetContactsId(id).Count();
        }

        public void Delete(int Id)
        {

            _contactcontext.Remove(GetContactId(Id));
            _contactcontext.SaveChanges();

        }
    }
}
