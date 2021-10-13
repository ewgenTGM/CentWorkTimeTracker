using System;
using System.ComponentModel.DataAnnotations;

namespace CentWorkTimeTracker.Models
{
    public enum ClaimType
    {
        Vacation,
        UnpaidedVacation,
        Sick,
        SickDays,
        Transfer
    }

    public enum ClaimStatus
    {
        Approved,
        Rejected,
        InProgress
    }

    public class Claim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ClaimType Type { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ClaimStatus ClaimStatus { get; private set; } = ClaimStatus.InProgress;

        public void Approve()
        {
            ClaimStatus = ClaimStatus.Approved;
        }

        public void Reject()
        {
            ClaimStatus = ClaimStatus.Rejected;
        }
    }
}