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

        public AuthController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepo = userRepository;
            _emailService = emailService;
        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult> Get()
        {
            ResponseModel<UserReadDto> responseModel = new ResponseModel<UserReadDto>();
            if (HttpContext.Session.Keys.Contains("userId"))
            {
                responseModel.Data = await _userRepo.GetUserById(int.Parse(HttpContext.Session.GetString("userId")));
                responseModel.Messages.Add("You Are authorized");
                return Ok(responseModel);
            }
            responseModel.Messages.Add("You Are not authorized");
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            ResponseModel<User> responseModel = new ResponseModel<User>();
            User user = await _userRepo.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                responseModel.Ok = false;
                responseModel.Errors.Add("Bad email and/or password");
                return BadRequest(responseModel);
            }
            if (!PasswordHelper.PasswordCompare(loginModel.Password, user.Password))
            {
                responseModel.Ok = false;
                responseModel.Errors.Add("Bad email and/or password");
                return BadRequest(responseModel);
            }
            Console.WriteLine(HttpContext.Session.Id);
            responseModel.Data = user;
            responseModel.Messages.Add("Login success");
            responseModel.Messages.Add(HttpContext.Session.Id);
            HttpContext.Session.SetString("userId", user.Id.ToString());
            return Ok(responseModel);
        }

        [HttpDelete]
        [Route("login")]
        public ActionResult Login()
        {
            if (HttpContext.Session.Keys.Contains("userId"))
            {
                HttpContext.Session.Clear();
                return Ok("Logout success");
            }
            return BadRequest("We are not knoew who You are");
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserReadDto>> Register(RegisterModel registerModel)
        {
            var valid = ModelState.IsValid;
            ResponseModel<UserReadDto> responseModel = new ResponseModel<UserReadDto>();
            var candidate = await _userRepo.GetUserByEmail(registerModel.Email);
            if (candidate != null)
            {
                responseModel.Ok = false;
                responseModel.Errors.Add($"User with email {registerModel.Email} allready exist");
                return BadRequest(responseModel);
            }
            var newUser = await _userRepo.AddUser(registerModel);

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