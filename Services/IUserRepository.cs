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
        Task<UserReadDto> GetUserById(int id);

        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<UserReadDto>> GetAllUsers();

        Task<UserReadDto> AddUser(RegisterModel user);
    }
}