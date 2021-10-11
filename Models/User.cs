using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Models
{
    public enum UserTypes
    {
        User,
        Manager
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public UserTypes UserType { get; set; } = UserTypes.User;
    }
}