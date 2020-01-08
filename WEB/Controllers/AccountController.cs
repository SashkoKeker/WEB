using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEB.DTO;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenSettings _appSettings;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, IMapper mapper, IOptions<TokenSettings> applicationSettings, ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
            _appSettings = applicationSettings.Value;
            _mapper = mapper;
        }

        [HttpGet("Claims")]
        public IActionResult GetClaims()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var claim = claims.First(x => x.Type == "UserID");
            return Ok(claim);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromBody]UserRegistration model)
        {
            if (ModelState.IsValid)
            {
               
                var user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                var profile = new Models.Profile { UserId = user.Id, NickName = model.NickName };
                _context.Profiles.Add(profile);
                _context.SaveChanges();
                _context.Users.Find(user.Id).Profile = profile;
                _context.SaveChanges();
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                await _userManager.AddToRoleAsync(user, "user");
                return Created("", "Пользователь успешно создан.");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("Moderators")]
        public async Task<IActionResult> GetModerators()
        {
            var moderators = await _userManager.GetUsersInRoleAsync("moderator");
            var _moderators = _mapper.Map<IEnumerable<UserDto>>(moderators);
            return Ok(_moderators);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            var _users = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(_users);
        }

        [HttpPost("Authorization")]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthorization model)
        {
            var user = await this._userManager.FindByEmailAsync(model.Email);
            var roleId = _context.UserRoles.First(x => x.UserId == user.Id).RoleId;
            var role = _context.Roles.First(x => x.Id == roleId).Name;

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("UserID", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, role));


            if (await this._userManager.CheckPasswordAsync(user, model.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySp$cialPassw0rd"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:5000",
                    audience: "https://localhost:5000",
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    Token = tokenString,
                    ExpiresIn = token.ValidTo,
                    Email = user.Email,
                    Id = user.Id
                });
            }

            return Unauthorized();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            User user = _context.Users.First(x => x.UserName == "admin");
            if (user != null)
                roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpGet("roles/{email}")]
        public async Task<IActionResult> GetRolesByEmail(string email)
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            User user = _context.Users.First(x => x.Email == email);
            if (user != null)
                roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }
}