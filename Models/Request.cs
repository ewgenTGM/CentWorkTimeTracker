using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentWorkTimeTracker.Models
{
    public enum RequestStatus
    {
        Approved,
        Rejected,
        InProgress
    }

    public abstract class Request
    {
        [Key]
        public Guid Id { get; set; }

        public string Discriminator { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
        public AppUser AppUser { get; set; }

        public RequestStatus RequestStatus { get; private set; } = RequestStatus.InProgress;

        public abstract int GetDayCount();

        public void Approve()
        {
            RequestStatus = RequestStatus.Approved;
        }

        public void Reject()
        {
            RequestStatus = RequestStatus.Rejected;
        }
    }
}