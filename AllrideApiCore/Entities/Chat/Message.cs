namespace AllrideApiCore.Entities.Chat
{
    public class Message
    {
        public int id {get;set;}
        public int sender_id { get; set; }
        public int recipient_id { get; set; }
        public int content_type { get; set; }
        public string message_content { get; set; }
        public DateTime created_at { get; set; }
    }
}
