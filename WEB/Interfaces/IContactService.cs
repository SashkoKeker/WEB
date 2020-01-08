using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Interfaces
{
    public interface IContactService
    {
        IQueryable<Contact> GetContacts();
        Contact GetContactId(int id);
        IQueryable<Contact> GetContactsId(string id);
        Contact GetContactsName(string Name);
        Contact CreateContact(Contact contact);
        int GetAmountByUserId(string id);
        void Delete(int id);

    }
}
