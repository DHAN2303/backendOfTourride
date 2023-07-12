namespace AllrideApiCore.Dtos.ResponseDtos
{
    public class GroupResponseDto
    {
        public string name { get; set; }
        public string backgroundCover_path { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public int group_rank { get; set; }
        public string group_admin { get; set; }
        public DateTime created_date { get; set; }
        public int member_count { get; set; }

    }
}
