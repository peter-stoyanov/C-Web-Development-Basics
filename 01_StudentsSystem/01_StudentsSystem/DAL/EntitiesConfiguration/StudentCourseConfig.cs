using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsSystem.Models;

namespace StudentsSystem.DAL.EntitiesConfiguration
{
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
        }
    }
}