using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class Seeder
    {
        private readonly UserManager<User> _usermanager;
        private readonly RoleManager<Role> _rolemanager;
        private readonly ApplicationContext _context;

        public Seeder(UserManager<User> usermanager, ApplicationContext context, RoleManager<Role> roleManager)
        {
            _usermanager = usermanager;
            _context = context;
            _rolemanager = roleManager;
        }
        public async Task Seed()
        {
            string[] roleNames = { "Admin", "User", "ProUser", "Moderator" };

            foreach (var x in roleNames)
            {
                if (_context.Roles.Where(r => r.Name == x).Count() == 0)
                {
                    await _rolemanager.CreateAsync(new Role { Name = x });
                }
            }
            _context.SaveChanges();

            var res = _context.Roles.First(x => x.Name == "Admin");
            User user = new User { Email = "sburchinskaya.2000@gmail.com", UserName = "admin" };
            await _usermanager.CreateAsync(user, "TeleBlurbDHvuehGyefv6527!");

            await _usermanager.AddToRoleAsync(user, "admin");
        }
    }
}