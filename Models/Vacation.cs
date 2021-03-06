using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    // Модель отпуска 25 дней в год
    public class Vacation : Request
    {
        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public override int GetDayCount() => (DateEnd - DateBegin).Days + 1;
    }
}