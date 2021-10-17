using CentWorkTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class FakeEmailService : IEmailService
    {
        private IUserRepository _userRepo;

        public FakeEmailService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public bool sendMessageToManager<T>(T request) where T : Request
        {          
            var user = _userRepo.GetUserById(request.UserId).Result;
            request.User = user;
            Console.WriteLine($"Send mail to manager.");
            Console.WriteLine(request.ToString());
            return true;
        }

        public bool sendMessageToUser<T>(T request) where T : Request
        {
            var user = _userRepo.GetUserById(request.UserId).Result;
            Console.WriteLine($"Send mail to user {user.Name} <{user.Email}>");
            Console.WriteLine(request.ToString());
            return true;
        }

        public bool sendRegisterEmail(User user)
        {
            Console.WriteLine($"Send mail to user {user.Name} <{user.Email}>");
            return true;
        }
    }
}