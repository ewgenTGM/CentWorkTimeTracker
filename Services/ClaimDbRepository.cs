using CentWorkTimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class ClaimDbRepository : IClaimsRepository
    {
        private readonly AppDbContext _db;

        public ClaimDbRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Claim> AddClaim(Claim claim)
        {
            try
            {
                await _db.Claims.AddAsync(claim);
                await _db.SaveChangesAsync();
                return claim;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<IEnumerable<Claim>> GetAllClaimsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Claim> GetAllInProgressClaim()
        {
            var claims = _db.Claims.Where(claim => claim.ClaimStatus == ClaimStatus.InProgress).Include(c => c.User).ToList();
            return claims;
        }

        public Task<Claim> GetClaimById(int claimId)
        {
            throw new NotImplementedException();
        }

        public async Task<Claim> SetClaimStatus(int claimId, ClaimStatus claimStatus)
        {
            var claim = await _db.Claims.FirstOrDefaultAsync(claim => claim.Id == claimId);
            if (claim == null)
            {
                return null;
            }

            switch (claimStatus)
            {
                case ClaimStatus.Approved:
                    claim.Approve();
                    break;

                case ClaimStatus.Rejected:
                    claim.Reject();
                    break;
            }
            await _db.SaveChangesAsync();
            return claim;
        }

        IEnumerable<Claim> IClaimsRepository.GetAllClaimsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}