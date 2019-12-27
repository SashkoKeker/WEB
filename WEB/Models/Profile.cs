using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class Profile : IdentityUser<Guid>
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string RestorePasswordCode { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
