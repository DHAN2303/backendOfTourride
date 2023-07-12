using AllrideApiCore.Entities;
using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.Chat
{
    public class GroupChat :BaseEntity
    {
        public string group_name { get; set; }
        public IFormFile group_image { get; set; }
        public string group_description { get; set; }
        
        #nullable enable
        public List<string>? group_members { get; set; }
    }
}
