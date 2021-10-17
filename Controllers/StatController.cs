//using CentWorkTimeTracker.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CentWorkTimeTracker.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class StatController : Controller
//    {
//        private readonly UserStatisticService _statService;

//        public StatController(UserStatisticService statService)
//        {
//            _statService = statService;
//        }

//        [HttpGet("sick/{id}")]
//        public IActionResult GetSickDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "Sick");
//            return Ok(count);
//        }

//        [HttpGet("sickdays/{id}")]
//        public IActionResult GetSickDaysDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "SickDays");
//            return Ok(count);
//        }

//        [HttpGet("transfer/{id}")]
//        public IActionResult GetTransferDaysDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "Transfer");
//            return Ok(count);
//        }

//        [HttpGet("vacation/{id}")]
//        public IActionResult GetVacationDaysDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "Vacation");
//            return Ok(count);
//        }

//        [HttpGet("unpaidedvacation/{id}")]
//        public IActionResult GetUnpaidedVacationDaysDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "UnpaidedVacation");
//            return Ok(count);
//        }

//        [HttpGet("wfh/{id}")]
//        public IActionResult GetWfhDaysDaysCountByUserId(int id)
//        {
//            int count = _statService.GetDaysCountByUserid(id, "WorkFromHome");
//            return Ok(count);
//        }
//    }
//}