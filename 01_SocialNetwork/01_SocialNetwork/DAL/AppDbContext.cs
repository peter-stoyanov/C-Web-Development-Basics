using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=.;Database=SocialNetwork;Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Friends)
                .WithOne(uf => uf)

            base.OnModelCreating(modelBuilder);
        }
    }
}
