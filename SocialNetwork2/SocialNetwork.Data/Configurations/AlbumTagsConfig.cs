using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Models;

namespace SocialNetwork.Data.Configurations
{
    public class AlbumTagsConfig : IEntityTypeConfiguration<AlbumTags>
    {
        public void Configure(EntityTypeBuilder<AlbumTags> modelBuilder)
        {
            modelBuilder
               .HasKey(at => new { at.AlbumId, at.TagId });

            modelBuilder
                .HasOne(at => at.Album)
                .WithMany(a => a.Tags)
                .HasForeignKey(at => at.AlbumId);

            modelBuilder
                .HasOne(at => at.Tag)
                .WithMany(t => t.Albums)
                .HasForeignKey(at => at.TagId);
        }
    }
}