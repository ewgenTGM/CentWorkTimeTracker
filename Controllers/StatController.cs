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
    [Route("api/[controller]")]
    public class StatController : Controller
    {
        private readonly UserStatisticService _statService;
        private readonly IUserRepository _userRepository;

        public StatController(UserStatisticService statService, IUserRepository userRepository)
        {
            _statService = statService;
            _userRepository = userRepository;
        }

        [HttpGet("sick/{id}")]
        public IActionResult GetSickDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "Sick");
            return Ok(count);
        }

        [HttpGet("sickdays/{id}")]
        public IActionResult GetSickDaysDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "SickDays");
            return Ok(count);
        }

        [HttpGet("transfer/{id}")]
        public IActionResult GetTransferDaysDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "Transfer");
            return Ok(count);
        }

        [HttpGet("vacation/{id}")]
        public IActionResult GetVacationDaysDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "Vacation");
            return Ok(count);
        }

        [HttpGet("unpaidedvacation/{id}")]
        public IActionResult GetUnpaidedVacationDaysDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "UnpaidedVacation");
            return Ok(count);
        }

        [HttpGet("wfh/{id}")]
        public IActionResult GetWfhDaysDaysCountByUserId(int id)
        {
            int count = _statService.GetDaysCountByUserid(id, "WorkFromHome");
            return Ok(count);
        }
        [HttpGet("{email}")]
        public async Task<ActionResult> GetStatisticByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest(new { message = "Use not found" });
            }
            var Sicks = _statService.GetDaysCountByUserid(user.Id, "Sick", true);
            var SickDays = _statService.GetDaysCountByUserid(user.Id, "SickDays", true);
            var SickDaysRemaining = 5 -_statService.GetDaysCountByUserid(user.Id, "Sick", true);
            var Vacations = _statService.GetDaysCountByUserid(user.Id, "Vacation", true);
            var VacationsRemaining = 25 - _statService.GetDaysCountByUserid(user.Id, "Vacation", true);
            var UnpaidedVacations = _statService.GetDaysCountByUserid(user.Id, "UnpaidedVacation", true);
            var Transfers = _statService.GetDaysCountByUserid(user.Id, "Transfer", true);
            return Ok(new {Sicks, SickDays, SickDaysRemaining, Vacations, VacationsRemaining, UnpaidedVacations, Transfers});
        }

        [HttpGet]
        public async Task<ActionResult> GetStatistic()
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return BadRequest(new { message = "Use not found" });
            }
            var Sicks = _statService.GetDaysCountByUserid(user.Id, "Sick", true);
            var SickDays = _statService.GetDaysCountByUserid(user.Id, "SickDays", true);
            var SickDaysRemaining = 5 - _statService.GetDaysCountByUserid(user.Id, "Sick", true);
            var Vacations = _statService.GetDaysCountByUserid(user.Id, "Vacation", true);
            var VacationsRemaining = 25 - _statService.GetDaysCountByUserid(user.Id, "Vacation", true);
            var UnpaidedVacations = _statService.GetDaysCountByUserid(user.Id, "UnpaidedVacation", true);
            var Transfers = _statService.GetDaysCountByUserid(user.Id, "Transfer", true);
            return Ok(new { Sicks, SickDays, SickDaysRemaining, Vacations, VacationsRemaining, UnpaidedVacations, Transfers });
        }

    }
}