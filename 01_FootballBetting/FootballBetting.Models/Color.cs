using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Team> PrimaryTeams { get; set; } = new List<Team>();
        public List<Team> SecondaryTeams { get; set; } = new List<Team>();
    }
}
