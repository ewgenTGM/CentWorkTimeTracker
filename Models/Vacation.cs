using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public enum VacationType
    {
        Paid = 0, // Оплачиваемый  дней в год
        Unpaid = 1 // За свой счёт
    }

    /// <summary>
    /// Модель отпуска
    /// </summary>
    public class Vacation : DaysBaseModel
    {
        public VacationType Type { get; set; }
        public int DaysCount => (To - From).Days;
    }
}