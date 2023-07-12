namespace AllrideApiCore.Dtos.ResponseDto
{
    public class ClubResponseDto
    {
        public string name { get; set; }
        public string club_admin { get; set; }
        public string backgroundCover_path { get; set; }
        public string profile_path { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public DateTime created_date { get; set; }
        public int member_count { get; set; }


    }
}
