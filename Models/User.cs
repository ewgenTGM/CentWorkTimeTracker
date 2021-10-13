using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentWorkTimeTracker.Models
{
    public enum UserTypes
    {
        User = 0,
        Manager = 100
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
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public UserTypes UserType { get; set; } = UserTypes.User;

        [JsonIgnore]
        public List<Claim> Claims { get; set; }
    }
}