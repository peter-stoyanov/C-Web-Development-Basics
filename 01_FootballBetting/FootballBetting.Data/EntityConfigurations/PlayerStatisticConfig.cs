using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder
                .HasKey(pg => new { pg.PlayerId, pg.GameId });

            builder
                .HasOne(pg => pg.Game)
                .WithMany(g => g.Statistics)
                .HasForeignKey(pg => pg.GameId);

            builder
                .HasOne(pg => pg.Player)
                .WithMany(g => g.Statistics)
                .HasForeignKey(pg => pg.PlayerId);
        }
    }
}
