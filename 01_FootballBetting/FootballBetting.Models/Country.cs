using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Country
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<CountryContinent> Continents { get; set; } = new List<CountryContinent>();

        public List<Town> Towns { get; set; } = new List<Town>();
    }
}
