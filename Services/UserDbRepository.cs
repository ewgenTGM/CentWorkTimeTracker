using AutoMapper;
using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Helpers;
using CentWorkTimeTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class UserDbRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UserDbRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _db = appDbContext;
            _mapper = mapper;
        }

        public async Task<UserReadDto> AddUser(RegisterModel registerModel)
        {
            User userToAdd = _mapper.Map<User>(registerModel);
            userToAdd.Password = PasswordHelper.GetHashedPassword(registerModel.Password);
            await _db.Users.AddAsync(userToAdd);
            int count = await _db.SaveChangesAsync();
            if (count == 0)
            {
                return null;
            }
            return _mapper.Map<UserReadDto>(userToAdd);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
            return user;
        }

        public async Task<UserReadDto> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
            return _mapper.Map<UserReadDto>(user);
        }
    }
}