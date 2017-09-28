using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string Initials { get; set; }

        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public decimal Budget { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
