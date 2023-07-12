namespace AllrideApiCore.Dtos.SocialMedia
{
    public class SocialMediaFollowDto
    {
        public int follower_id { get; set; }
        public int followed_id { get; set; }
    }


    public class SocialMediaUnFollowDto
    {
        public int follower_id { get; set; }
        public int followee_id { get; set; }
    }
}
