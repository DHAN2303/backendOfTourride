using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.RequestDto
{
    public class GroupUpdateChangePhotoDto 
    {
        public int GroupId { get; set; }
        public IFormFile File { get; set; }

    }
}
