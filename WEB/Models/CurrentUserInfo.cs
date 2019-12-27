using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WEB.Models
{
    public class CurrentUserInfo : ClaimsIdentity
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public CurrentUserInfo(IHttpContextAccessor accessor)
        {
            var identity = accessor.HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            Id = Guid.Parse(claims.First(x => x.Type == "UserID").Value);
            Role = claims.First(x => x.Type == ClaimTypes.Role).Value;
        }
    }
}
