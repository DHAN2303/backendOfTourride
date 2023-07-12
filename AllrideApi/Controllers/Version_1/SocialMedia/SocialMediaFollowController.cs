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

    public class SocialMediaFollowController: ControllerBase
    {
        private readonly ISocialMediaFollowService _socialMediaFollow;

        public SocialMediaFollowController(ISocialMediaFollowService socialMediaFollow)
        {
            _socialMediaFollow = socialMediaFollow;
        }

        [HttpPost]
        [Route("addFollower")]
        public async Task<IActionResult> addFollower([FromBody] SocialMediaFollowDto socialMediaFollow)
        {
            var respBody = _socialMediaFollow.AddFollower(socialMediaFollow);
            return Utils.GetInstance().getGlobalResponse(respBody);

        }


        [HttpDelete]
        [Route("unFollower")]
        public async Task<IActionResult> unFollower([FromBody] SocialMediaUnFollowDto socialMediaUnFollow)
        {
            var respBody = _socialMediaFollow.UnFollower(socialMediaUnFollow);
            return Utils.GetInstance().getGlobalResponse(respBody);
        }
        
        [HttpGet("getUserFriends")]
        public IActionResult GetUserFriends(SocialMediaFollowDto socialMediaFollowDto)
        {
            var result = _socialMediaFollow.GetUserFriends(socialMediaFollowDto);
            return result.Status == false ? BadRequest(result.ErrorEnums) : Ok(result.Data);
        }

    }
}
