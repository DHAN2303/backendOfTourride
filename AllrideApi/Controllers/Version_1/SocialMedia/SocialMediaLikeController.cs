using AllrideApi.Controllers.Global;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Services.Abstract.SocialMedia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.SocialMedia
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SocialMediaLikeController : ControllerBase
    {
        private readonly ISocialMediaLikeService _socialMediaLikeService;

        public SocialMediaLikeController(ISocialMediaLikeService socialMediaLikeService)
        {
            _socialMediaLikeService = socialMediaLikeService;
        }

        [HttpPost]
        [Route("postLike")]
        public Object postLike([FromBody] SocialMediaLikeDto socialMediaLikeDto)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var respBody = _socialMediaLikeService.PostLike(socialMediaLikeDto, Convert.ToInt32(userId));
            return Utils.GetInstance().getGlobalResponse(respBody);
        }


        [HttpDelete]
        [Route("postUnLike")]
        public Object postUnLike([FromBody] SocialMediaUnLikeDto socialMediaUnLikeDto)
        {
            var userId = HttpContext.User.Claims.First().Value;
            var respBody = _socialMediaLikeService.PostUnLike(socialMediaUnLikeDto, Convert.ToInt32(userId));
            return Utils.GetInstance().getGlobalResponse(respBody);
        }
    }
}
