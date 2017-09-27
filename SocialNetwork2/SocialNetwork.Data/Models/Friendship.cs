namespace SocialNetwork.Data.Models
{
    public class Friendship
    {
        public int FromFriendId { get; set; }
        public User FromFriend { get; set; }

        public int ToFriendId { get; set; }
        public User ToFriend { get; set; }
    }
}