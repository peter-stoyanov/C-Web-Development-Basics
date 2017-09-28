﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; }
    }
}
