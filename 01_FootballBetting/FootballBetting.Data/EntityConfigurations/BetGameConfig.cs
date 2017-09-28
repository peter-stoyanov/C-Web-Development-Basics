using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class BetGameConfig : IEntityTypeConfiguration<BetGame>
    {
        public void Configure(EntityTypeBuilder<BetGame> builder)
        {
            builder
                .HasKey(pg => new { pg.BetId, pg.GameId });

            builder
                .HasOne(pg => pg.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(pg => pg.GameId);

            builder
                .HasOne(pg => pg.Bet)
                .WithMany(g => g.Games)
                .HasForeignKey(pg => pg.BetId);
        }
    }
}
