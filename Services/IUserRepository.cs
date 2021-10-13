using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);

        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<User>> GetAllUsers();

        Task<User> AddUser(RegisterModel user);
    }
}