using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public enum VacationTypes
    {
        Ill,
        
    }
    public class VacationBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        public int DaysCount => (DateStart - DateEnd).Days;
        public string Description { get; set; }

    }
}
