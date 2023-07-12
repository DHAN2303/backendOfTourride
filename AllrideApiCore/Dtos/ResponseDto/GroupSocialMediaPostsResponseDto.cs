namespace AllrideApiCore.Dtos.ResponseDtos
{
    public class GroupSocialMediaPostsResponseDto
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public string HashTag { get; set; }
        public string ImagePath { get; set; }
        public string UserProfilePath { get; set; }
        public List<GroupSocialPostCommentDto> GroupSocialPostCommentDtos { get; set; }   

    }

    public class GroupSocialPostCommentDto
    {
        public string UserName { get; set; }
        public string UserPp { get; set; }
        public string Comment { get; set; }
    }
}
