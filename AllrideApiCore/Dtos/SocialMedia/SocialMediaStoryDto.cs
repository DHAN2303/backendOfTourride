using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.SocialMedia
{
    public class SocialMediaStoryDto
    {
        public int user_id { get; set; }
        public IFormFile file { get; set; }
        public string caption { get; set; }
        public string location { get; set; }

    }


    public class SocialMediaDeleteStoryDto
    {
        public int story_id { get; set; }
    }

    public class SocialMediaUpdateStoryDto
    {
        public int story_id { get; set; }
#nullable enable
        public string? caption { get; set; }
#nullable enable
        public string? location { get; set; }
    }
}
