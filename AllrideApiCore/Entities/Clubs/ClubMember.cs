namespace AllrideApiCore.Entities.Chat.Clubs
{
    public class ClubMember
    {
        public int id { get; set; }
        public int club_id { get; set; }
        public int user_id { get; set; }
        public int role { get; set; }
        public DateTime joined_date{ get; set; }
    }
}
