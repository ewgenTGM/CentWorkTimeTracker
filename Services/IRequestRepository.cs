using CentWorkTimeTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IRequestRepository
    {
        Task<T> AddRequest<T>(T request) where T : Request;

        Task<Request> SetRequestStatus(int id, RequestStatus claimStatus);

        List<Request> GetAllRequestsByUserId(int id);

        Task<Request> GetRequestById(int id);

        List<Request> GetAllInProgressRequests();

        List<Request> GetAllApprovedRequests();

        List<Request> GetAllRejectedRequests();
    }
}