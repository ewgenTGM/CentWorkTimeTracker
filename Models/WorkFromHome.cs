using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    // Модель работы из дома, всегда Approved
    public class WorkFromHome : Request
    {
        public DateTime Date { get; set; }

        public WorkFromHome()
        {
            Approve();
        }

        public override int GetDayCount() => 1;
    }
}