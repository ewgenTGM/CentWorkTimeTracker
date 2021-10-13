using CentWorkTimeTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IClaimsRepository
    {
        Task<Claim> AddClaim(Claim claim);

        Task<Claim> SetClaimStatus(int claimId, ClaimStatus claimStatus);

        IEnumerable<Claim> GetAllClaimsByUserId(int userId);

        Task<Claim> GetClaimById(int claimId);

        List<Claim> GetAllInProgressClaim();
    }
}