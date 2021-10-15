using CentWorkTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Services
{
    public interface IEmailService
    {
        bool sendMessageToManager<T>(T claim) where T : Claim;

        bool sendMessageToUser<T>(T claim) where T : Claim;

        bool sendRegisterEmail(User user);
    }
}