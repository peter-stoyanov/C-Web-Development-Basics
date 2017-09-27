using SocialNetwork.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [Password]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200, MinimumLength = 5)]
        public string Email { get; set; }

        [MaxFileSize(1048576)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<User> Friends { get; set; } = new List<User>();
    }
}
