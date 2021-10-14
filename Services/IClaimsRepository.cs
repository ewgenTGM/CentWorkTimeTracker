using CentWorkTimeTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IClaimsRepository
    {
        Task<T> AddClaim<T>(T claim) where T : Claim;

        Task<Claim> SetClaimStatus(int claimId, ClaimStatus claimStatus);

        List<Claim> GetAllClaimsByUserId(int userId);

        Task<Claim> GetClaimById(int claimId);

        List<Claim> GetAllInProgressClaim();

        List<Claim> GetAllApprovedClaim();

        List<Claim> GetAllRejectedClaim();
    }
}