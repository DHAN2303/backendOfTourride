

namespace AllrideApiCore.Entities.Clubs
{
    public class ClubMessage
    {
        public int id { get; set; }
        public int club_id { get; set; }
        public int sender_id { get; set; }
        public string message_content { get; set; }
        public int content_type { get; set; }
        public DateTime created_at { get; set; }
    }
}
