using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Caption { get; set; }

        [Required]
        public string Path { get; set; }

        public List<AlbumPictures> Albums { get; set; } = new List<AlbumPictures>();
    }
}