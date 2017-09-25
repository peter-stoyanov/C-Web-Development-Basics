using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentsSystem.DAL.EntitiesConfiguration;
using StudentsSystem.Models;
using System;
using System.Linq;

namespace StudentsSystem.DAL
{
    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=.;Database=StudentSystem;Integrated Security=true")
                .ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitialSetup(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new StudentCourseConfig());
            modelBuilder.ApplyConfiguration(new ResourceConfig());
            modelBuilder.ApplyConfiguration(new HomeworkConfig());
            modelBuilder.ApplyConfiguration(new LicenseConfig());

            base.OnModelCreating(modelBuilder);
        }

        private static void InitialSetup(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = "tbl_" + entity.ClrType.Name;

                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(String)))
                {
                    property.SetMaxLength(50);
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}