using AutoMapper;
using CentWorkTimeTracker.Dtos;
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

        public async Task<UserReadDto> AddUser(UserAddDto user)
        {
            var addedUser = await _db.Users.AddAsync(_mapper.Map<User>(user));
            var mapped = _mapper.Map<UserReadDto>(addedUser);
            await _db.SaveChangesAsync();
            return mapped;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}