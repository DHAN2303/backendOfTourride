
namespace AllrideApiCore.Entities.SocialMedia
{
    public class SocialMediaFollow
    {
        public int id { get; set; }
        public int follower_id { get; set; }
        public int followed_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
