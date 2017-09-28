using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Squadnumber { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public string PositionId { get; set; }
        public Position Position { get; set; }

        public bool IsCurrentlyInjured { get; set; }

        public List<PlayerGame> Games { get; set; } = new List<PlayerGame>();

        public List<PlayerStatistic> Statistics { get; set; } = new List<PlayerStatistic>();
    }
}
