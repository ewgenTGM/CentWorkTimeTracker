using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public class AddWorkFromHomeModel
    {
        [Required]
        public DateTime Date { get; set; }
    }
}