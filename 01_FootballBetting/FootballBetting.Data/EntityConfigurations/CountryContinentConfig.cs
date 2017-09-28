using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class CountryContinentConfig : IEntityTypeConfiguration<CountryContinent>
    {
        public void Configure(EntityTypeBuilder<CountryContinent> builder)
        {
            builder
                .HasKey(ck => new { ck.ContinentId, ck.CountryId });

            builder
                .HasOne(ck => ck.Country)
                .WithMany(c => c.Continents)
                .HasForeignKey(ck => ck.CountryId);

            builder
                .HasOne(ck => ck.Continent)
                .WithMany(c => c.Countries)
                .HasForeignKey(ck => ck.ContinentId);
        }
    }
}
