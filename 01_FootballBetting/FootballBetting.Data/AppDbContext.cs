using FootballBetting.Data.EntityConfigurations;
using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballBetting.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionType> CompetitionTypes { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ResultPrediction> ResultPredictions { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=.;Database=FootballBetting;Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BetConfig());
            modelBuilder.ApplyConfiguration(new BetGameConfig());
            modelBuilder.ApplyConfiguration(new CountryContinentConfig());
            modelBuilder.ApplyConfiguration(new GameConfig());
            modelBuilder.ApplyConfiguration(new PlayerConfig());
            modelBuilder.ApplyConfiguration(new PlayerGameConfig());
            modelBuilder.ApplyConfiguration(new PlayerStatisticConfig());
            modelBuilder.ApplyConfiguration(new TeamConfig());
            modelBuilder.ApplyConfiguration(new TownConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            SetupTableNamesAndCascadeDelete(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetupTableNamesAndCascadeDelete(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = "tbl_" + entity.ClrType.Name;

                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}
