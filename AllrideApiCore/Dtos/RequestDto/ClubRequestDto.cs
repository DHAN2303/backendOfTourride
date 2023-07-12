using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.RequestDto
{
    public class ClubRequestDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
#nullable enable
        public List<string>? ClubMembers { get; set; }
    }
}
