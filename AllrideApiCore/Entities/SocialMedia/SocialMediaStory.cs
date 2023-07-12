
namespace AllrideApiCore.Entities.SocialMedia
{
    public class SocialMediaStory
    {
        public int id { get; set; }
        public int user_id { get; set; }
#nullable enable
        public string? caption { get; set; }
        public string media_url { get; set; }
#nullable enable
        public string? location_info { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
