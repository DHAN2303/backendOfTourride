using AllrideApi.Controllers.Global;
using AllrideApiChat.Functions.Compress;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.SocialMedia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AllrideApi.Controllers.Version_1.SocialMedia
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequestSizeLimit(100_000_000)]//100 mb limit

    public class SocialMediaStoryController : ControllerBase
    {
        private readonly IPosts _posts;
        private readonly ISocialMediaStoryService _mediaStoryService;

        public SocialMediaStoryController(IPosts posts, ISocialMediaStoryService mediaStoryService)
        {
            _posts = posts;
            _mediaStoryService = mediaStoryService;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("story")]
        public Object SocialMediaStory([FromBody] SocialMediaStoryDto socialStory)
        {
            var file = socialStory.file;
            SocialMediaStoryImageCompress imageCompress = new SocialMediaStoryImageCompress(_posts);
            SocialMediaStoryVideoCompress videoCompress = new SocialMediaStoryVideoCompress(_posts);

            var extens = Path.GetExtension(file.FileName);
           

            try
            {
                string savedPath = "";
                if (extens == ".mp4" || extens == ".avi" || extens == ".mov" || extens == ".m4v" || extens == ".3gp")
                    savedPath = videoCompress.CompressVideo(socialStory, 0.5);
                else if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressImage(socialStory, 25);

                return Ok(CustomResponse<object>.Success(savedPath, true));
            }
            catch
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }

        [HttpPut]
        [Route("updateStory")]
        public Object SocialMediaDelteStory([FromBody] SocialMediaUpdateStoryDto socialMediaUpdate)
        {
            var respBody = _mediaStoryService.UpdatePost(socialMediaUpdate);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }


        [HttpDelete]
        [Route("deleteStory")]
        public Object SocialMediaDelteStory([FromBody] SocialMediaDeleteStoryDto socialMediaDelete)
        {
            var respBody = _mediaStoryService.DeletePost(socialMediaDelete);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }


        [HttpGet]
        [Route("fetchStory")]
        public async Task<IActionResult> SocialMediaFetchStory(int userId)
        {
            var respBody = _mediaStoryService.FetchStory(userId);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }
    }
}
