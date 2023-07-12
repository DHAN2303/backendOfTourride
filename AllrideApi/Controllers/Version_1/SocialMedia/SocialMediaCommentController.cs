using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Services.Abstract.SocialMedia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AllrideApi.Controllers.Global;

namespace AllrideApi.Controllers.Version_1.SocialMedia
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialMediaCommentController : ControllerBase
    {
        private readonly ISocialMediaCommentService _socialMediaComment;

        public SocialMediaCommentController(ISocialMediaCommentService socialMediaComment)
        {
            _socialMediaComment = socialMediaComment;
        }

        [HttpPost]
        [Route("postComment")]
        public async Task<IActionResult> sendComment([FromBody] SocialMediaCommentsDto socialMediaComments)
        {
            var respBody = _socialMediaComment.SendCommentToPost(socialMediaComments);
            return Utils.GetInstance().getGlobalResponse(respBody);

        }


        [HttpPut]
        [Route("editComment")]
        public async Task<IActionResult> EditComment([FromBody] SocialMediaEditCommensDto socialMediaEdit)
        {
            var respBody = _socialMediaComment.EditComment(socialMediaEdit);
            return Utils.GetInstance().getGlobalResponse(respBody);

        }


        [HttpDelete]
        [Route("deleteComment")]
        public async Task<IActionResult> DeleteComment([FromBody] SocialMediaDeleteCommentsDto socialMediaDelete)
        {
            var respBody = _socialMediaComment.DeleteComment(socialMediaDelete);
            return Utils.GetInstance().getGlobalResponse(respBody);

        }

    }
}
