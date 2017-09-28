using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Position
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
