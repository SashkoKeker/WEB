﻿using System;
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
        IQueryable<Contact> GetContctsId(Guid id);
        IQueryable<Contact> GetContctsName(string Name);
        Contact CreateContact(Contact contact);
        int GetAmountByUserId(Guid id);
        void Delete(int id);

    }
}
