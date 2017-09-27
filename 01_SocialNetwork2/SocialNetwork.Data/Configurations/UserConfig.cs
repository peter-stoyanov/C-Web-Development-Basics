using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Models;

namespace SocialNetwork.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.FromFriendships)
                .WithOne(fr => fr.FromFriend)
                .HasForeignKey(fr => fr.FromFriendId);

            builder
                .HasMany(u => u.ToFriendships)
                .WithOne(fr => fr.ToFriend)
                .HasForeignKey(fr => fr.ToFriendId);

            builder
               .HasMany(u => u.AlbumsOwned)
               .WithOne(a => a.Owner)
               .HasForeignKey(a => a.OwnerId);
        }
    }
}