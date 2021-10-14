using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Helpers
{
    public static class DaysHelper
    {
        public static bool IsWorkDay(DateTime date)
        {
            return !(date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
        }
    }
}