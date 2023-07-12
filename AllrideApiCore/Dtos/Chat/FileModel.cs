using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.Chat
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile file { get; set; }
        public int sender_id { get; set; }
        public int? receive_id { get; set; }
        public int? group_id { get; set; }
        public int chat_type { get; set; }
    }
}
