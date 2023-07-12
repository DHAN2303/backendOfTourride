using AllrideApiCore.Dtos.Chat;
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
        private readonly ILogger<FetchMessages> _logger;

        public FetchMessages(ILogger<FetchMessages> logger, AllrideApiDbContext context, IUserMessageService userMessageService, IHttpContextAccessor httpContextAccessor)
        {
            _userMessageService = userMessageService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet]
        [Route("fetchMessages")]
        public IActionResult fetchMessages([FromQuery] OldMessages oldMessages)
        {
            try
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
                    return StatusCode(500, response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " FetchMessages  -->  fetchMessages  METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }
        [HttpGet("allFriendsMessages")]
        public IActionResult allfriendsMessage()
        {
            try
            {
                var userId = HttpContext.User.Claims.First()?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                //// UserId'yi kullanarak yapmak istediğiniz işlemler
                bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

                if (isUserIdTypeInt == false)
                {
                    return Unauthorized();
                }
                var response = _userMessageService.GetUserFriendsMessages(UserId);
                if (response.Status)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " FetchMessages  -->  fetchMessages  METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
    }
}
