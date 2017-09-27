using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Models;

namespace SocialNetwork.Data.Configurations
{
    public class AlbumPicturesConfig : IEntityTypeConfiguration<AlbumPictures>
    {
        public void Configure(EntityTypeBuilder<AlbumPictures> modelBuilder)
        {
            modelBuilder
               .HasKey(ap => new { ap.AlbumId, ap.PictureId });

            modelBuilder
                .HasOne(ap => ap.Album)
                .WithMany(a => a.Pictures)
                .HasForeignKey(ap => ap.AlbumId);

            modelBuilder
                .HasOne(ap => ap.Picture)
                .WithMany(p => p.Albums)
                .HasForeignKey(ap => ap.PictureId);
        }
    }
}