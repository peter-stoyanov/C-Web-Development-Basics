using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            builder
                .HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);
        }
    }
}
