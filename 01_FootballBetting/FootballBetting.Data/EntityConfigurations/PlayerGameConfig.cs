using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PlayerGameConfig : IEntityTypeConfiguration<PlayerGame>
    {
        public void Configure(EntityTypeBuilder<PlayerGame> builder)
        {
            builder
                .HasKey(pg => new { pg.PlayerId, pg.GameId });

            builder
                .HasOne(pg => pg.Game)
                .WithMany(g => g.Players)
                .HasForeignKey(pg => pg.GameId);

            builder
                .HasOne(pg => pg.Player)
                .WithMany(g => g.Games)
                .HasForeignKey(pg => pg.PlayerId);
        }
    }
}
