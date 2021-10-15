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

        public bool sendMessageToManager<T>(T claim) where T : Claim
        {
            User user = _userRepo.GetUserById(claim.UserId).Result;
            Console.WriteLine($"Send mail to manager {claim.User.Name} <{claim.User.Email}>");
            Console.WriteLine(claim.ToString());
            return true;
        }

        public bool sendMessageToUser<T>(T claim) where T : Claim
        {
            Console.WriteLine($"Send mail to user {claim.User.Name} <{claim.User.Email}>");
            Console.WriteLine(claim.ToString());
            return true;
        }

        public bool sendRegisterEmail(User user)
        {
            Console.WriteLine($"Send mail to user {user.Name} <{user.Email}>");
            return true;
        }
    }
}