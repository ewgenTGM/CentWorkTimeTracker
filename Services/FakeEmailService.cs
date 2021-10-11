using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public class FakeEmailService : IEmailService
    {
        public Task<bool> sendMessage(MailMessage email)
        {
            return Task.FromResult(true);
        }
    }
}