using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEB.DTO;
using WEB.Interfaces;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly CurrentUserInfo _info;
        private readonly ILogger _logger;
        private readonly IProfileService _profiles;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;

        public ProfileController(IProfileService profiles, IMapper mapper, ILogger<ProfileController> logger, CurrentUserInfo info, IHostingEnvironment environment)
        {
            _profiles = profiles;
            _mapper = mapper;
            _logger = logger;
            _info = info;
            _environment = environment;
        }

        [HttpGet("ReadByUserId/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            string message = $"{DateTime.Now} : Reading all profiles by {_info.Role}: {_info.Id}";
            _logger.LogInformation($"Reading all profiles by {_info.Role}: {_info.Id}");

            var profiles = _profiles.GetProfileByUserId(id);
            return Ok(_mapper.Map<ProfileDTO>(profiles));
        }

        [HttpGet("ReadById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var profiles = _profiles.GetProfileById(id);
            return Ok(_mapper.Map<ProfileDTO>(profiles));
        }


        [Authorize(Roles = "Developer")]
        [HttpGet("ReadMyInfo")]
        public IActionResult GetMyInfo()
        {
            var profile = _profiles.GetMyInfo();
            return Ok(_mapper.Map<ProfileDTO>(profile));
        }

        [Authorize(Roles = "Developer")]
        [HttpPatch("Update")]
        public IActionResult UpdateRequest(ProfileDTO profile)
        {
            var prof = _mapper.Map<Models.Profile>(profile);
            return StatusCode((int)HttpStatusCode.NoContent, _profiles.UpdateProfile(prof));
        }
    }
}