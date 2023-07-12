using Microsoft.AspNetCore.Http;

namespace AllrideApiCore.Dtos.RequestDto
{
    public class ClubUpdateChangePhotoDto 
    {
        public int ClubId { get; set; }
        public IFormFile File { get; set; }
        public string Location { get; set; }

    }
}
