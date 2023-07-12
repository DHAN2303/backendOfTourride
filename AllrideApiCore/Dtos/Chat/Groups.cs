using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.Chat
{
    public class Groups
    {
        public string group_name {  get; set; }

        public IFormFile group_image { get; set; }

        public string group_description { get; set; }

        public int admin { get; set; }

        public List<string>? group_members { get; set; }

    }
}
