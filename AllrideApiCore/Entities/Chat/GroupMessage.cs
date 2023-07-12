namespace AllrideApiCore.Entities.Chat
{
    public class GroupMessage
    {
        public int id { get; set; }
        public int group_id { get; set; }
        public int sender_id { get; set; }
        public string message_content { get; set; }
        public int content_type { get; set; }
        public DateTime created_at { get; set; }
    }
}
