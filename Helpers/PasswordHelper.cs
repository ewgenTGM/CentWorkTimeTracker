using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace CentWorkTimeTracker.Helpers
{
    public static class PasswordHelper
    {
        public static string GetHashedPassword(string pass) => BC.HashPassword(pass);

        public static bool PasswordCompare(string pass, string hashedPass)
        {
            return BC.Verify(pass, hashedPass);
        }
    }
}