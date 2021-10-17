using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CentWorkTimeTracker.Models
{
    public class AppUser: IdentityUser
    {
        public List<Request> Requests { get; set; }
    }
}
