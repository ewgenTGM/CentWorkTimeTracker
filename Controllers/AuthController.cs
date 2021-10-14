using AutoMapper;
using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, IEmailService emailService, IMapper mapper)
        {
            _userRepo = userRepository;
            _emailService = emailService;
            _mapper = mapper;
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
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            User user = await _userRepo.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "Bad login/password" });
            }
            if (!PasswordHelper.PasswordCompare(loginModel.Password, user.Password))
            {
                return BadRequest(new { Message = "Bad login/password" });
            }
            HttpContext.Session.SetInt32("userId", user.Id);
            HttpContext.Session.SetInt32("userRole", (int)user.UserType);
            return Ok(_mapper.Map<UserReadDto>(user));
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
            var candidate = await _userRepo.GetUserByEmail(registerModel.Email);
            if (candidate != null)
            {
                return new BadRequestObjectResult(new { Message = "User allready exist" });
            }
            var newUser = await _userRepo.AddUser(registerModel);

            if (newUser == null)
            {
                return new BadRequestObjectResult(new { Message = "Something went wrong((" });
            }
            await _emailService.sendMessage(new System.Net.Mail.MailMessage());
            return Ok(_mapper.Map<UserReadDto>(newUser));
        }
    }
}