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

        public int GetDaysCountByUserid(int userId, string discriminator)
        {
            int daysCount = 0;
            var sicks = _claimsRepository.GetAllApprovedClaim().Where(s => s.Discriminator == discriminator && s.UserId == userId).ToList();
            if (sicks.Count != 0)
            {
                foreach (var sick in sicks)
                {
                    daysCount += sick.GetDayCount();
                }
            }
            return daysCount;
        }
    }
}