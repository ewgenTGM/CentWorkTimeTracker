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
        bool sendMessageToManager<T>(T request) where T : Request;

        bool sendMessageToUser<T>(T request) where T : Request;

        bool sendRegisterEmail(User user);
    }
}