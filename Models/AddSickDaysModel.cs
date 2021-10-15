using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AddSickDaysModel
    {
        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        public string Description { get; set; }
    }
}