using System;
using System.Collections.Generic;

namespace FootballBetting.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public decimal BetMoney { get; set; }

        public DateTime DatePut { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<BetGame> Games { get; set; } = new List<BetGame>();
    }
}
