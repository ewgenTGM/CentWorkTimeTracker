using CentWorkTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Dtos
{
    public class UserReadDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public UserTypes UserType { get; set; } = UserTypes.User;
    }
}