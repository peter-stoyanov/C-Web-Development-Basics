using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballBetting.Data.EntityConfigurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            builder
                .HasOne(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);
        }
    }
}
