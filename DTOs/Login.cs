﻿using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}