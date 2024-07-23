﻿using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
