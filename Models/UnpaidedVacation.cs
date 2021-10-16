using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class UnpaidedVacation : Request
    {
        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string Description { get; set; }

        public override int GetDayCount() => (DateEnd - DateBegin).Days + 1;
    }
}