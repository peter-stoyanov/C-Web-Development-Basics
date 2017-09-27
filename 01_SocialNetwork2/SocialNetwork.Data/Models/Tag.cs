using SocialNetwork.Data.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Tag]
        [Required]
        public string Name { get; set; }

        public List<AlbumTags> Albums { get; set; } = new List<AlbumTags>();

        public override string ToString()
        {
            return this.Name;
        }
    }
}