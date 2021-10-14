using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AddSickModel
    {
        [Required]
        public DateTime DateBegin { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public string DocNumber { get; set; }
    }
}