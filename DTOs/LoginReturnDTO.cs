﻿namespace Inventory_Management_API.DTOs
{
    public class LoginReturnDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
