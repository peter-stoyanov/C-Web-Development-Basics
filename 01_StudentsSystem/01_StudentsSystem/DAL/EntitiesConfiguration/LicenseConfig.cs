using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsSystem.Models;

namespace StudentsSystem.DAL.EntitiesConfiguration
{
    public class LicenseConfig : IEntityTypeConfiguration<License>
    {
        public void Configure(EntityTypeBuilder<License> builder)
        {
            builder
                .HasOne(l => l.Resource)
                .WithMany(r => r.Licenses)
                .HasForeignKey(l => l.ResourceId);
        }
    }
}