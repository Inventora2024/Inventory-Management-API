﻿using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class UserDisplayDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Role { get; set; }
    }
}