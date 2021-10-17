//using CentWorkTimeTracker.Helpers;
//using CentWorkTimeTracker.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CentWorkTimeTracker.Services
//{
//    public class UserDbRepository : IUserRepository
//    {
//        private readonly AppDbContext _db;

//        public UserDbRepository(AppDbContext appDbContext)
//        {
//            _db = appDbContext;
//        }

//        public async Task<User> AddUser(RegisterModel registerModel)
//        {
//            User userToAdd = new User()
//            {
//                Email = registerModel.Email,
//                Name = registerModel.Name,
//                Password = PasswordHelper.GetHashedPassword(registerModel.Password)
//            };

//            await _db.Users.AddAsync(userToAdd);
//            int count = await _db.SaveChangesAsync();
//            if (count == 0)
//            {
//                return null;
//            }
//            return userToAdd;
//        }

//        public async Task<IEnumerable<User>> GetAllUsers()
//        {
//            var users = await _db.Users.ToListAsync();
//            return users;
//        }

//        public async Task<User> GetUserByEmail(string email)
//        {
//            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
//            return user;
//        }

//        public async Task<User> GetUserById(int id)
//        {
//            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
//            return user;
//        }
//    }
//}