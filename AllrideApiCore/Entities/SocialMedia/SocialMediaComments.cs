
namespace AllrideApiCore.Entities.SocialMedia
{
    public class SocialMediaComments
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public int post_id { get; set; }
        public string text { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
