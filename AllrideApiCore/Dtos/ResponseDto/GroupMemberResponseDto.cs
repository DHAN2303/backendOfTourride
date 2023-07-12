namespace AllrideApiCore.Dtos.ResponseDto
{
    public class GroupMemberResponseDto
    {
        public int group_id { get; set; }
        public int user_id { get; set; }
        public int role { get; set; } // 0 creator, 1 admin, 2 kullanıcı
        public DateTime joined_date { get; set; }
        public int active { get; set; }
    }
}
