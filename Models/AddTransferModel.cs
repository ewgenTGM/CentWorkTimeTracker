using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AddTransferModel
    {
        [Required]
        public DateTime DayFrom { get; set; }

        [Required]
        public DateTime DayTo { get; set; }

        public string Description { get; set; }
    }
}