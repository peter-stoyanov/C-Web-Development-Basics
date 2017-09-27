using SocialNetwork.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [Password]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Email]
        [MaxLength(200)]
        [MinLength(5)]
        public string Email { get; set; }

        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes")]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<Friendship> FromFriendships { get; set; } = new List<Friendship>();

        public List<Friendship> ToFriendships { get; set; } = new List<Friendship>();

        public List<Album> AlbumsOwned { get; set; } = new List<Album>();

        public List<AlbumParticipates> AlbumsParticipate { get; set; } = new List<AlbumParticipates>();
    }
}