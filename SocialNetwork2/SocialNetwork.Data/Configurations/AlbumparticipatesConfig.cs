using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Models;

namespace SocialNetwork.Data.Configurations
{
    public class AlbumParticipatesConfig : IEntityTypeConfiguration<AlbumParticipates>
    {
        public void Configure(EntityTypeBuilder<AlbumParticipates> modelBuilder)
        {
            modelBuilder
                .HasKey(ap => new { ap.AlbumId, ap.UserId });

            modelBuilder
                .HasOne(ap => ap.Album)
                .WithMany(a => a.Participants)
                .HasForeignKey(ap => ap.AlbumId);

            modelBuilder
                .HasOne(ap => ap.User)
                .WithMany(u => u.AlbumsParticipate)
                .HasForeignKey(ap => ap.UserId);
        }
    }
}