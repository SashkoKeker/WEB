using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? UserDateRegistration { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<Order> orders { get; set; }
        public List<Contact> contacts { get; set; }
        public List<Contact> contactes { get; set; }
        public Profile Profile { get; set; }
        public User()
        {
            UserRoles = new List<UserRole>();
            orders = new List<Order>();
            contacts = new List<Contact>();
            contactes = new List<Contact>();

        }
    }
}
