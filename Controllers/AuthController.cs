using AutoMapper;
using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(IEmailService emailService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        [Route("me")]
        public async Task<ActionResult> Get()
        {
            var aaa = _signInManager.Context.User.Identity.IsAuthenticated;
            if (!aaa)
            {
                return Unauthorized(new { Message = "You are not authorized" });
            }
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var candidate = await _userManager.FindByEmailAsync(loginModel.Email);

            if (candidate == null)
            {
                return BadRequest(new { Message = "Bad login or password" });
            }
            var signInResult = await _signInManager.PasswordSignInAsync(candidate, loginModel.Password, true, false);

            if (!signInResult.Succeeded)
            {
                return BadRequest(new { Message = "Bad login or password" });
            }
            var aaa = HttpContext.User.Identity.Name;

            return Ok(new { Name = candidate.UserName, candidate.Email, userType = "Добавить ROLE" });
        }

        [HttpDelete]
        [Route("login")]
        public async Task<ActionResult> Login()
        {            
            await _signInManager.SignOutAsync();
            return Ok("Logout success");
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {

            if (!registerModel.Email.EndsWith("@centaurea.io"))
            {
                return BadRequest(new { Message = "Email address must be in @centoria.io domain." });
            }
            var candidate = await _userManager.FindByEmailAsync(registerModel.Email);
            if (candidate != null)
            {
                return BadRequest(new { Message = "User allready exist" });
            }
            var user = new AppUser() { Email = registerModel.Email, UserName = registerModel.Name };
            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            _emailService.sendRegisterEmail(user);
            return Ok(user);
        }
    }
}