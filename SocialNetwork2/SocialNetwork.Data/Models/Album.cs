using SocialNetwork.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Colors BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public List<AlbumParticipates> Participants { get; set; } = new List<AlbumParticipates>();

        public List<AlbumPictures> Pictures { get; set; } = new List<AlbumPictures>();

        public List<AlbumTags> Tags { get; set; } = new List<AlbumTags>();
    }
}