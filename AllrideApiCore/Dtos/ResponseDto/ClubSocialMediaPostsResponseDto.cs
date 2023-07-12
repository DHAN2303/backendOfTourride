namespace AllrideApiCore.Dtos.ResponseDto
{
    public class ClubSocialMediaPostsResponseDto
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public string HashTag { get; set; }
        public string ImagePath { get; set; }
        public string UserProfilePath { get; set; }
        public List<ClubSocialPostCommentDto> ClubSocialPostCommentDtos { get; set; }   

    }

    public class ClubSocialPostCommentDto
    {
        public string UserName { get; set; }
        public string UserPp { get; set; }
        public string Comment { get; set; }
    }
}
