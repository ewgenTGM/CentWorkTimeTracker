using CentWorkTimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class RequestDbRepository : IRequestRepository
    {
        private readonly AppDbContext _db;

        public RequestDbRepository(AppDbContext db)
        {
            _db = db;
        }

        // Добавление заявки
        public async Task<T> AddRequest<T>(T request) where T : Request
        {
            try
            {
                await _db.AddAsync(request);
                await _db.SaveChangesAsync();
                return request;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Список всех подтвержденных заявок
        public List<Request> GetAllApprovedRequests()
        {
            var claims = _db.Requests.Where(request => request.RequestStatus == RequestStatus.Approved).Include(c => c.User).ToList();
            return claims;
        }

        // Список всех подтвержденных пользователя
        public Task<IEnumerable<Request>> GetAllRequestsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        // Список всех необработанных заявок
        public List<Request> GetAllInProgressRequests()
        {
            var requests = _db.Requests.Where(request => request.RequestStatus == RequestStatus.InProgress).Include(c => c.User).ToList();
            return requests;
        }

        // Список всех отклоненных заявок
        public List<Request> GetAllRejectedRequests()
        {
            var requests = _db.Requests.Where(request => request.RequestStatus == RequestStatus.Rejected).Include(c => c.User).ToList();
            return requests;
        }

        // Получить заявку по id
        public Task<Request> GetRequestById(int id)
        {
            var request = _db.Requests.Where(request => request.Id == id).Include(c => c.User).First();
            return Task.FromResult(request);
        }

        // Для менеджера, установка статуса заявки
        public async Task<Request> SetRequestStatus(int id, RequestStatus requestStatus)
        {
            var request = await _db.Requests.Include(c => c.User).SingleOrDefaultAsync(request => request.Id == id);
            if (request == null)
            {
                return null;
            }

            switch (requestStatus)
            {
                case RequestStatus.Approved:
                    request.Approve();
                    break;

                case RequestStatus.Rejected:
                    request.Reject();
                    break;

                default:
                    break;
            }
            await _db.SaveChangesAsync();
            return request;
        }

        List<Request> IRequestRepository.GetAllRequestsByUserId(int id)
        {
            var requests = _db.Requests.Where(c => c.UserId == id).Include(c => c.User).ToList();
            return requests;
        }

    }
}