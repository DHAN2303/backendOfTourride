using AllrideApiCore.Dtos.Chat;
using AllrideApiCore.Entities.Chat;
using AllrideApiRepository;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.UserMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FetchMessages : ControllerBase
    {
        protected readonly AllrideApiDbContext _context;
        private readonly IUserMessageService _userMessageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FetchMessages(AllrideApiDbContext context, IUserMessageService userMessageService, IHttpContextAccessor httpContextAccessor)
        {
            _userMessageService = userMessageService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("fetchMessages")]
        public IActionResult fetchMessages([FromQuery] OldMessages oldMessages)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.First().Value);
            //var user_id_2 = oldMessages.firendId;
            //var dataList = _context.messages.Where(m=>((m.sender_id == user_id_1 && m.recipient_id == user_id_2) || (m.sender_id == user_id_2 && m.recipient_id == user_id_1))).ToList();
            // return dataList;
            var response = _userMessageService.GetUserMessages(userId, oldMessages.firendId);
            if (response.Status)
            {
                return Ok(response);
            }
            else
            {
                return Ok(response);
            }

        }
        [HttpGet("allFriendsMessages")]
        public IActionResult allfriendsMessage()
        {
            var response =  _userMessageService.GetUserFriendsMessages(64);
            return Ok(CustomResponse<object>.Success(response, true));
        }
    }
}
