using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryTeams)
                .HasForeignKey(t => t.PrimaryKitColorId);

            builder
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);

            builder
                .HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);
        }
    }
}
