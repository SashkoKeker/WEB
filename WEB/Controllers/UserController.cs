using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WEB.DTO;
using WEB.Models;
using WEB.Services;
using WEB.Interfaces;
namespace WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userservice, IMapper mapper, UserManager<User> userManager)
        {
            _userService = userservice;
            _mapper = mapper;
            _userManager = userManager;
        }

        //[Authorize(Roles = "Admin, Moderator")]
        [HttpGet("Read")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [Authorize(Roles = "User")]
        [HttpGet("FindByLogin")]
        public IActionResult FindByLogin(string login)
        {
            return Ok(_mapper.Map<UserDto>(_userService.GetByLogin(login)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("FindById")]
        public IActionResult FindById(Guid Id)
        {
            return Ok(_mapper.Map<UserDto>(_userService.GetById(Id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("CreateModerator/{id}")]
        public async Task<IActionResult> CreateModerator(Guid id)
        {
            var user = _userService.GetById(id);
            await _userManager.RemoveFromRoleAsync(user, "user");
            await _userManager.AddToRoleAsync(user, "moderator");

            return Ok("Роль указанного пользователя успешно изменена.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("CreateProUser")]
        public async Task<IActionResult> CreateProUser(Guid id)
        {
            var user = _userService.GetById(id);
            await _userManager.RemoveFromRoleAsync(user, "user");
            await _userManager.AddToRoleAsync(user, "prouser");

            return Ok("Роль указанного пользователя успешно изменена.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("DeleteModerator/{id}")]
        public async Task<IActionResult> RemoveModerator(Guid id)
        {
            var user = _userService.GetById(id);
            await _userManager.RemoveFromRoleAsync(user, "moderator");
            await _userManager.AddToRoleAsync(user, "user");

            return Ok("Роль указанного пользователя успешно изменена.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("DeleteProUser")]
        public async Task<IActionResult> RemoveProUser(Guid id)
        {
            var user = _userService.GetById(id);
            await _userManager.RemoveFromRoleAsync(user, "prouser");
            await _userManager.AddToRoleAsync(user, "user");

            return Ok("Роль указанного пользователя успешно изменена.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteAccount(Guid id)
        {
            _userService.Delete(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}