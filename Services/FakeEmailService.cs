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
        public bool sendMessageToManager<T>(T claim) where T : Claim
        {
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
    }
}