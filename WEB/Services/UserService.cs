using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WEB.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace WEB.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.Users;

            return users;
        }


        public User GetById(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        public User GetByLogin(string login)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == login);
            return user;
        }

        public void Delete(Guid Id)
        {
            var id = _context.UserRoles.First(x => x.UserId == Id).RoleId;
            var role = _context.Roles.First(x => x.Id == id);

            if (role.Name == "Developer")
            {
                _context.Remove(GetById(Id));
                _context.SaveChanges();
            }
        }
    }
}
