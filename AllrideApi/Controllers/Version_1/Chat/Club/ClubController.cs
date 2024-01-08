using AllrideApi.Controllers.Version_1.Chat.Group;
using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Enums;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.ClubsInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AllrideApi.Controllers.Version_1.Chat.Clup
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;
        private readonly IPosts _posts;
        private readonly ILogger<GroupsController> _logger;
        public ClubController(IClubService clubService, ILogger<GroupsController> logger, IPosts posts)
        {
            _clubService = clubService;
            _logger = logger;   
            _posts = posts;
        }


        [HttpPost]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("createClub")]
        public IActionResult Club([FromForm] ClubRequestDto clubsRequestDto)
        {
            try
            {
                _posts.CreateNewClub(clubsRequestDto);
                return Ok(CustomResponse<object>.Success(SuccessEnumResponse.RegisterSuccessfull, true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException.ToString());
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NotGroupCreated, false));
            }
        }

        [HttpGet("clubsMedias")]
        public IActionResult getMedia(int ClubId)
        {
            var result = _clubService.GetClubMedia(ClubId);
            if (result.Status)
            {
                return Ok(CustomResponse<object>.Success(result.Data, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }
        }


        [HttpGet("getClubDetail")]
        public IActionResult GetClubDetail(int ClubId)
        {
            var result = _clubService.GetClubDetail(ClubId);
            if (result.Status)
            {
                return Ok(CustomResponse<object>.Success(result.Data, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                //return result.Status == false ? BadRequest(result.ErrorEnums) : Ok(result.Data);
            }
        }

        [HttpGet("getGlobalClubs")]
        public IActionResult GetGlobalClubs(int ClubId, int Type = 1)
        {
            var result = _clubService.GetGlobalClubs(ClubId, Type);
            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteClub")]
        public IActionResult DeleteClub(int ClubId)
        {

            var result = _clubService.DeleteClub(ClubId);
            return Ok(result);

        }

        [HttpDelete]
        [Route("deleteUserInClub")]
        public IActionResult DeleteUserInClub(int ClubId, int UserId)
        {
            var result = _clubService.DeleteUserInClub(ClubId, UserId);
            return Ok(result);


        }

        [HttpGet("getClubUser")]
        public IActionResult GetClubUserDetail(int ClubId)
        {
            var result = _clubService.GetClubUserDetail(ClubId);
            return Ok(result);
        }

        [HttpGet("allMemberedClubsByUser")]
        public IActionResult GetAllMemberedGroupsByUser()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var response = _clubService.GetMemberedClubsByUser(Convert.ToInt32(userId));
            return Ok(response);

        }

        [HttpGet]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("getMessages")]
        public IActionResult GetMessages(int club_id)
        {
            var response = _clubService.GetClubMessage(club_id);
            return Ok(response);
        }
    }
}
