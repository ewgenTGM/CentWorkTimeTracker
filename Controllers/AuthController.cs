using AutoMapper;
using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult> Get()
        {
            if (!HttpContext.Session.Keys.Contains("userId"))
            {
                return Unauthorized(new { Message = "You are not authorized" });
            }
            var user = await _userRepo.GetUserById(HttpContext.Session.GetInt32("userId").Value);
            return Ok(new { name = user.Name, email = user.Email, userType = user.UserType });
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            User user = await _userRepo.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "Bad login or password" });
            }
            if (!PasswordHelper.PasswordCompare(loginModel.Password, user.Password))
            {
                return BadRequest(new { Message = "Bad login or password" });
            }
            HttpContext.Session.SetInt32("userId", user.Id);
            HttpContext.Session.SetInt32("userRole", (int)user.UserType);
            return Ok(new { name = user.Name, email = user.Email, userType = user.UserType });
        }

        [HttpDelete]
        [Route("login")]
        public ActionResult Login()
        {
            HttpContext.Session?.Clear();
            return Ok("Logout success");
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {

            if (!registerModel.Email.EndsWith("@centoria.io"))
            {
                return BadRequest(new {Message = "Email address must be in @centoria.io domain."});
            }
            var candidate = await _userRepo.GetUserByEmail(registerModel.Email);
            if (candidate != null)
            {
                return BadRequest(new { Message = "User allready exist" });
            }
            var user = await _userRepo.AddUser(registerModel);

            if (user == null)
            {
                return BadRequest(new { Message = "Something went wrong((" });
            }
            _emailService.sendRegisterEmail(user);
            return Ok(new { name = user.Name, email = user.Email, userType = user.UserType });
        }
    }
}