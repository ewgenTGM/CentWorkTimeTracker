﻿using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : Controller
    {
        private readonly IClaimsRepository _claimsRepo;

        public ClaimController(IClaimsRepository claimsRepo)
        {
            _claimsRepo = claimsRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var claim = await _claimsRepo.GetClaimById(id);
            return Ok(claim);
        }

        [HttpGet("user/{id}")]
        public ActionResult GetByUserId(int id)
        {
            var claims = _claimsRepo.GetAllClaimsByUserId(id);
            return Ok(claims);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult GetAll()
        {
            var claims = _claimsRepo.GetAllInProgressClaim();
            return Ok(claims);
        }

        [HttpGet]
        [Route("all/rejected")]
        public ActionResult GetRejected()
        {
            var claims = _claimsRepo.GetAllRejectedClaim();
            return Ok(claims);
        }

        [HttpGet]
        [Route("all/approved")]
        public ActionResult GetApproved()
        {
            var claims = _claimsRepo.GetAllApprovedClaim();
            return Ok(claims);
        }

        [HttpPost]
        [Route("add/vacation")]
        public async Task<ActionResult> AddVacation([FromBody] AddVacationModel model)
        {
            if (!HttpContext.Session.GetInt32("userId").HasValue)
            {
                return Unauthorized();
            }
            int userId = HttpContext.Session.GetInt32("userId").Value;
            Vacation vacation = new Vacation()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd
            };
            var added = await _claimsRepo.AddClaim(vacation);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/unpaidedvacation")]
        public async Task<ActionResult> AddUnpaidedVacation([FromBody] AddUnpaidedVacationModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            UnpaidedVacation unpaidedVacation = new UnpaidedVacation()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                Description = model.Description
            };
            var added = await _claimsRepo.AddClaim(unpaidedVacation);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/sick")]
        public async Task<ActionResult> AddSick([FromBody] AddSickModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            Sick sick = new Sick()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                DocNumber = model.DocNumber
            };
            var added = await _claimsRepo.AddClaim(sick);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/sickdays")]
        public async Task<ActionResult> AddSickDays([FromBody] AddSickDaysModel model)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            SickDays sickDays = new SickDays()
            {
                UserId = userId,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                Description = model.Description
            };
            var added = await _claimsRepo.AddClaim(sickDays);
            return Ok(added);
        }

        [HttpPost]
        [Route("add/transfer")]
        public async Task<ActionResult> AddTransfer([FromBody] AddTransferModel model)
        {
            if (!DaysHelper.IsWorkDay(model.DayFrom) || DaysHelper.IsWorkDay(model.DayTo))
            {
                return BadRequest("Check dates");
            }
            if (model.DayFrom > model.DayTo)
            {
                return BadRequest("Check dates");
            }
            int userId = HttpContext.Session.GetInt32("userId").Value;
            Transfer transfer = new Transfer()
            {
                UserId = userId,
                DayFrom = model.DayFrom,
                DayTo = model.DayTo,
                Description = model.Description
            };
            var added = await _claimsRepo.AddClaim(transfer);
            return Ok(added);
        }

        [HttpGet("approve/{id}")]
        public async Task<ActionResult> ApproveClaim(int id)
        {
            var claim = await _claimsRepo.SetClaimStatus(id, ClaimStatus.Approved);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }

        [HttpGet("reject/{id}")]
        public async Task<ActionResult> RejectClaim(int id)
        {
            var claim = await _claimsRepo.SetClaimStatus(id, ClaimStatus.Rejected);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }
    }
}