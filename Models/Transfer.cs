using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    //Модель переноса рабочего дня
    public class Transfer : Claim
    {
        public DateTime DayFrom { get; set; }

        public DateTime DayTo { get; set; }

        public string Description { get; set; }

        public override int GetDayCount() => 1;
    }
}