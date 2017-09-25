using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsSystem.Models;

namespace StudentsSystem.DAL.EntitiesConfiguration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasMany(s => s.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId);
        }
    }
}