
namespace AllrideApiCore.Dtos.SocialMedia
{
    public class SocialMediaCommentsDto
    {
        public string user_id { get; set; }
        public int post_id {get;set;}
        public string text { get; set; }
    }

    public class SocialMediaEditCommensDto
    {
        public int comment_id { get; set; }
        public string text { get; set; }
    }

    public class SocialMediaDeleteCommentsDto
    {
        public int post_id { get; set; }
        public int comment_id { get; set; }
    }
}
