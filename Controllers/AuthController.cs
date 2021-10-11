using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailService _emailService;

        public AuthController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepo = userRepository;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("me")]
        public ActionResult Get()
        {
            return Ok("Works, but we don't know hwo You are...");
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserReadDto>> Register([FromBody] UserAddDto user)
        {
            ResponseModel<UserReadDto> responseModel = new ResponseModel<UserReadDto>();
            if (!ModelState.IsValid)
            {
            }
            var newUser = await _userRepo.AddUser(user);
            if (newUser == null)
            {
                responseModel.Ok = false;
                responseModel.Errors.Add("Error bla-bla-bla");
                return BadRequest(responseModel);
            }

            responseModel.Data = newUser;
            responseModel.Messages.Add("User has been created");
            await _emailService.sendMessage(new System.Net.Mail.MailMessage());
            return Ok(responseModel);
        }
    }
}