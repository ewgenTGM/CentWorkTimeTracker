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

        // Добавление заявки
        public async Task<T> AddClaim<T>(T claim) where T : Claim
        {
            try
            {
                await _db.AddAsync(claim);
                await _db.SaveChangesAsync();
                return claim;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Список всех подтвержденных заявок
        public List<Claim> GetAllApprovedClaim()
        {
            var claims = _db.Claims.Where(claim => claim.ClaimStatus == ClaimStatus.Approved).Include(c => c.User).ToList();
            return claims;
        }

        // Список всех подтвержденных пользователя
        public Task<IEnumerable<Claim>> GetAllClaimsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        // Список всех необработанных заявок
        public List<Claim> GetAllInProgressClaim()
        {
            var claims = _db.Claims.Where(claim => claim.ClaimStatus == ClaimStatus.InProgress).Include(c => c.User).ToList();
            return claims;
        }

        // Список всех отклоненных заявок
        public List<Claim> GetAllRejectedClaim()
        {
            var claims = _db.Claims.Where(claim => claim.ClaimStatus == ClaimStatus.Rejected).Include(c => c.User).ToList();
            return claims;
        }

        // Получить заявку по id
        public Task<Claim> GetClaimById(int claimId)
        {
            var claim = _db.Claims.Where(c => c.Id == claimId).Include(c => c.User).First();
            return Task.FromResult(claim);
        }

        // Для менеджера, установка статуса заявки
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

                default:
                    break;
            }
            await _db.SaveChangesAsync();
            return claim;
        }

        List<Claim> IClaimsRepository.GetAllClaimsByUserId(int userId)
        {
            var claims = _db.Claims.Where(c => c.UserId == userId).ToList();
            return claims;
        }
    }
}