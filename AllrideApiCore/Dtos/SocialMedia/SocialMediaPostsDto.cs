using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.SocialMedia
{
    public class SocialMediaPostsDto
    {
        public int user_id { get; set; }
        public IFormFile file { get; set; }
        public string caption { get; set; }
        public string location { get; set; }

    }


    public class SocialMediaDeletePostDto
    {
        public int post_id { get; set; }
    }

    public class SocialMediaUpdatePostDto
    {
        public int post_id { get; set; }
#nullable enable
        public string? caption { get; set; }
#nullable enable
        public string? location { get; set; }
    }
}
