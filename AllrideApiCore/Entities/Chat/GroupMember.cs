namespace AllrideApiCore.Entities.Chat
{
    public class GroupMember
    {
        public int id { get; set; }
        public int group_id { get; set; }
        public int user_id { get; set; }
        public int role { get; set; }
        public DateTime joined_date{ get; set; }
    }
}
