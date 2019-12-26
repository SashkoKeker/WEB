using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Role : IdentityRole<Guid>
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public List<UserRole> UserRoles { get; set; }
        public Role()
        {
            UserRoles = new List<UserRole>();
        }
    }
}
