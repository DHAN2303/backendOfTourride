namespace AllrideApiCore.Dtos.Select
{
    public class GetAllNewsResponseDto
    {
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int ActionType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
