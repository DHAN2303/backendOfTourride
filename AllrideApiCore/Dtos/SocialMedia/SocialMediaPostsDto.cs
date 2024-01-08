using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.SocialMedia
{
    public class SocialMediaPostsDto
    { //where 0: group || 1: person || 2: club
        public int user_id { get; set; }
        public IFormFile file { get; set; }
        public string caption { get; set; }
        public string location { get; set; }
        public int where { get; set; }
        public int whereId { get; set; }

    }


    public class SocialMediaDeletePostDto
    {
        public int post_id { get; set; }
    }

    public class SocialMediaUpdatePostDto
    {
        public int post_id { get; set; }
        public string? caption { get; set; }
        public string? location { get; set; }
    }
}
