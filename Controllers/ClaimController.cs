using CentWorkTimeTracker.Models;
using CentWorkTimeTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Claim>> Get()
        {
            var claims = _claimsRepo.GetAllInProgressClaim();
            return claims;
        }

        [HttpPost]
        [Route("add")]
        public async Task<Claim> AddClaim(Claim claim)
        {
            int userId = HttpContext.Session.GetInt32("userId").Value;
            await _claimsRepo.AddClaim(claim);
            return claim;
        }
    }
}