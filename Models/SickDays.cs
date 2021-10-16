using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    //Модель больничного без справки
    public class SickDays : Claim
    {
        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string Description { get; set; }

        public override int GetDayCount() => (DateEnd - DateBegin).Days + 1;
        public SickDays()
        {
            Approve();
        }
    }
}