using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class UserStatisticService
    {
        private readonly IClaimsRepository _claimsRepository;

        public UserStatisticService(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }

        public int GetDaysCountByUserid(int userId, string discriminator, bool includeInPropgressClaims = false)
        {
            int daysCount = 0;
            var claims = _claimsRepository.GetAllApprovedClaim().Where(s => s.Discriminator == discriminator && s.UserId == userId).ToList();
            if (includeInPropgressClaims)
            {
                claims.AddRange(_claimsRepository.GetAllInProgressClaim().Where(s => s.Discriminator == discriminator && s.UserId == userId).ToList());
            }
            if (claims.Count != 0)
            {
                foreach (var sick in claims)
                {
                    daysCount += sick.GetDayCount();
                }
            }
            return daysCount;
        }


    }
}