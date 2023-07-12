namespace AllrideApiCore.Dtos.Select
{
    public class NewsAllResponseDto
    {
        public List<GetAllNewsResponseDto> _news { get; set; }
        public int TotalCount { get; set; }

    }
}
