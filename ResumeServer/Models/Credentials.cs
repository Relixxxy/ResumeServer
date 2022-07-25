﻿using ResumeServer.Models;
using System.ComponentModel.DataAnnotations;

namespace ResumeServer.Models
{
    public class Credentials : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Role { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public User User { get; set; }
    }
}
