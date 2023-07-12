using AllrideApi.Controllers.Global;
using AllrideApiChat.Functions.Compress;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.SocialMedia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllrideApi.Controllers.Version_1.SocialMedia
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequestSizeLimit(100_000_000)]//100 mb limit
    public class SocialMediaPostsController : ControllerBase
    {
        private readonly IPosts _posts;
        private readonly ISocialMediaPostsService _mediaPostsService;
        public SocialMediaPostsController(IPosts posts, ISocialMediaPostsService mediaPostsService)
        {
            _posts = posts;
            _mediaPostsService = mediaPostsService;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> SocialMediaPost([FromForm] SocialMediaPostsDto socialPosts)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            var file = socialPosts.file;
            SocialMediaPostsImageCompress imageCompress = new SocialMediaPostsImageCompress(_posts);
            SocialMediaPostVideoCompress videoCompress = new SocialMediaPostVideoCompress(_posts);

            var extens = Path.GetExtension(file.FileName);
            
            try
            {
                string savedPath = "";

                if (extens == ".mp4" || extens == ".avi" || extens == ".mov" || extens == ".m4v" || extens == ".3gp")
                    savedPath = videoCompress.CompressVideo(socialPosts, 0.5, Convert.ToInt32(userId));
                else if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressImage(socialPosts, 25, Convert.ToInt32(userId));

                return Ok(CustomResponse<object>.Success(savedPath, true));
            }
            catch
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }

        [HttpPut]
        [Route("updatePost")]
        public async Task<IActionResult> SocialMediaUpdatePost([FromForm] SocialMediaUpdatePostDto socialMediaUpdate)
        {
            var respBody = _mediaPostsService.UpdatePost(socialMediaUpdate);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }


        [HttpDelete]
        [Route("deletePost")]
        public async Task<IActionResult> SocialMediaDeletePost([FromBody] SocialMediaDeletePostDto socialMediaDelete)
        {
            var respBody = _mediaPostsService.DeletePost(socialMediaDelete);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }


        [HttpGet]
        [Route("fetchPost")]
        public async Task<IActionResult> SocialMediaFetchPost()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var respBody = _mediaPostsService.FetchPost(Convert.ToInt32(userId));
            //return Utils.GetInstance().getGlobalResponse(respBody);
            if (respBody != null)
            {
                try
                {
                    return Ok(CustomResponse<object>.Success(respBody, true));
                }
                catch
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }
    }
}
