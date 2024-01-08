namespace AllrideApiCore.Dtos.ResponseDto
{
    public class GroupResponseDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public int    group_rank { get; set;  }
        public string group_admin { get; set; }
        public DateTime created_date { get; set; }
    }
}
