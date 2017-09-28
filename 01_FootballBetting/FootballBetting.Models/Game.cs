using System;
using System.Collections.Generic;

namespace FootballBetting.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public DateTime PlayedOn { get; set; }

        public decimal HomeTeamWinBetRate { get; set; }
        public decimal AwayTeamWinBetRate { get; set; }
        public decimal DrawBetRate { get; set; }

        public int RoundId { get; set; }
        public Round Round { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public List<PlayerGame> Players { get; set; } = new List<PlayerGame>();

        // needed ?
        public List<PlayerStatistic> Statistics { get; set; }

        public List<BetGame> Bets { get; set; } = new List<BetGame>();
    }
}
