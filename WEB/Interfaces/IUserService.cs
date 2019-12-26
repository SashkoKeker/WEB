using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        User GetByLogin(string login);
        void Delete(Guid Id);
    }
}
