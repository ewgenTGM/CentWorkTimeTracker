using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IEmailService
    {
        Task<bool> sendMessage(MailMessage email);
    }
}