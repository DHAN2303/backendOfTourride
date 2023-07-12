
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.User
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InviteController : ControllerBase
    {
        private IPushNotificationService _notificationService;
        public InviteController(IPushNotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("invite")]
        public IActionResult InviteUser(int senderUserId, int receiveUserId, int where, int whereId)
        {
            var response = where == 0 ? _notificationService.InviteGroup(senderUserId, receiveUserId, whereId) : _notificationService.InviteClub(senderUserId, receiveUserId, whereId);
            if (response.Status)
            {
                return Ok(CustomResponse<object>.Success(true));
            }
            return Ok(CustomResponse<object>.Success(false)); 
        }

        [HttpGet]
        [Route("canBeInvitedUsers")]
        public IActionResult GetCanBeInvitedUsers(int where, int whereId)
        {
            var user_id = HttpContext.User.Claims.First().Value;
            var response = _notificationService.GetCanInviteUsers(Convert.ToInt32(user_id), where, whereId);
            if (response.Status)
            {
                return Ok(response);
            }
            return Ok(CustomResponse<Boolean>.Success(false));
        }

        [HttpDelete]
        [Route("inviteDecline")]
        public IActionResult InviteRep(int inviteId, bool inviteRep)
        {
            var response = _notificationService.InviteReply(inviteId, inviteRep);
            if (response.Status)
            {
                return Ok(CustomResponse<Boolean>.Success(true));
            }
            return Ok(CustomResponse<Boolean>.Success(false));
        }
    }
}
