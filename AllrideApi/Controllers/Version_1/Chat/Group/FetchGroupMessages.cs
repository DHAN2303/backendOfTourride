using AllrideApiRepository;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Chats.GroupChats;
using AllrideApiService.Services.Abstract.UserMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.Chat.Group
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FetchGroupMessages : ControllerBase
    {
        protected readonly AllrideApiDbContext _context;
        private readonly IGroupChatService _groupChatService;
        private readonly IUserMessageService _userMessageService;
        private readonly ILogger<FetchGroupMessages> _logger;
        public FetchGroupMessages(AllrideApiDbContext context, IUserMessageService userMessageService,
            IGroupChatService groupChatService, ILogger<FetchGroupMessages> logger)
        {
            _context = context;
            _userMessageService = userMessageService;
            _groupChatService = groupChatService;
            _logger = logger;
        }

        [HttpGet]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("fetchMessages")]
        public IActionResult fetchGroupMessages(int group_id)
        {
            try
            {
                var response = _userMessageService.GetGroupUserMessages(group_id);
                if (response.Status)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(" Fetch Group Message Controller GetGroupUserMessages METHOD Log Error  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        // 26 Mayıs 2023  // Bir kullanıcının ekli oldu�u gruptaki tüm kullanıcıları getiriyor
        [HttpGet("allUserGroupsMessages")]
        public IActionResult allUserGroupsMessage()
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _groupChatService.GetUserGroupsMessages(UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(" Fetch Group Message Controller allUserGroupsMessage METHOD Log Error  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }
    }
}
