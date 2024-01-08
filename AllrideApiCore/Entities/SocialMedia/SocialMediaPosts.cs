
namespace AllrideApiCore.Entities.SocialMedia
{
    public class SocialMediaPosts
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string caption { get; set; }
        public string media_url { get; set; }
        public string location_info { get; set; }
        public int? likes_count { get; set; }
        public int? comments_count { get; set; }
        public int where { get; set; }
        public int whereId { get; set; }
        public List<int> LikedByUsers { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
