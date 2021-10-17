//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CentWorkTimeTracker.Services
//{
//    public class UserStatisticService
//    {
//        private readonly IRequestRepository _requestsRepository;

//        public UserStatisticService(IRequestRepository requestsRepository)
//        {
//            _requestsRepository = requestsRepository;
//        }

//        public int GetDaysCountByUserid(int userId, string discriminator, bool includeInPropgressRequests = false)
//        {
//            int daysCount = 0;
//            var requests = _requestsRepository.GetAllApprovedRequests().Where(s => s.Discriminator == discriminator && s.UserId == userId).ToList();
//            if (includeInPropgressRequests)
//            {
//                requests.AddRange(_requestsRepository.GetAllInProgressRequests().Where(s => s.Discriminator == discriminator && s.UserId == userId).ToList());
//            }
//            if (requests.Count != 0)
//            {
//                foreach (var request in requests)
//                {
//                    daysCount += request.GetDayCount();
//                }
//            }
//            return daysCount;
//        }


//    }
//}