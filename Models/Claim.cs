using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentWorkTimeTracker.Models
{
    public enum ClaimStatus
    {
        Approved,
        Rejected,
        InProgress
    }

    public abstract class Claim
    {
        [Key]
        public int Id { get; set; }

        public string Discriminator { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ClaimStatus ClaimStatus { get; private set; } = ClaimStatus.InProgress;

        public abstract int GetDayCount();

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