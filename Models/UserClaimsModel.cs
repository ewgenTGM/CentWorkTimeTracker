using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class UserClaimsModel
    {
        public User User { get; set; }
        public List<Claim> Claims { get; set; }
    }
}