namespace AllrideApiCore.Dtos.ResponseDto
{
    public class ClubSocialPostDto
    {
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public string[] PostImagePath { get; set; }
        public string HashTag { get; set; }
        public int LikeUnlikeCount { get; set; }
        public string UserName { get; set; }
        //public List<string> Comment { get; set; }

    }
}
