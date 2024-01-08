using AllrideApiCore.Dtos.Chat;
using AllrideApiService.Enums;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace AllrideApi.Controllers.Version_1.Chat.Group
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IPosts _posts;
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService, IPosts posts, ILogger<GroupsController> logger)
        {
            _posts = posts;
            _logger = logger;
            _groupService = groupService;

        }


        [HttpPost]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("createGroup")]
        public IActionResult Group([FromForm] Groups groups)
        {
            try
            {
                _posts.PostNewGroup(groups.group_name, groups.group_image, groups.group_description, groups.group_members, groups.admin);
                return Ok(CustomResponse<object>.Success(SuccessEnumResponse.RegisterSuccessfull, true));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException.ToString());
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NotGroupCreated, false));
            }
        }

        [HttpGet("groupsMedias")]
        public IActionResult GetMedia(int GroupId)
        {
            var result = _groupService.GetGroupMedia(GroupId);
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

        [HttpGet("getGroupDetail")]
        public IActionResult GetGroupDetail(int GroupId)
        {
            var result = _groupService.GetGroupDetail(GroupId);
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
        [HttpGet("getGlobalGroups")]
        public IActionResult GetGlobalGroups(int GroupId, int Type=1)
        {
            var result = _groupService.GetGlobalGroups(GroupId, Type);
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

        [HttpDelete]
        [Route("deleteGroup")]
        public IActionResult DeleteGroup(int GroupId)
        {

            var result = _groupService.DeleteGroup(GroupId);
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

        [HttpDelete]
        [Route("deleteUserInGroup")]
        public IActionResult DeleteUserInGroup(int GroupId, int UserId)
        {

            var result = _groupService.DeleteUserInGroup(GroupId, UserId);
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

        [HttpGet("getGroupUser")]
        public IActionResult GetGroupUserDetail(int GroupId)
        {
            var result = _groupService.GetGroupUserDetail(GroupId);
            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
                //return result.Status == false ? BadRequest(result.ErrorEnums) : Ok(result.Data);
            }
        }

        [HttpGet("groupSearch")]
        public IActionResult SearchGroup(string userName, int GroupId)   
        {
            var searchResult = _groupService.SearchUserGroup(userName, GroupId);
            return Ok(searchResult);

        }

        [HttpGet("allMemberedGroupsByUser")]
        public IActionResult GetAllMemberedGroupsByUser()
        {
            var userId = HttpContext.User.Claims.First().Value;
            var response = _groupService.GetMemberedGroupsByUser(Convert.ToInt32(userId));
            return Ok(response);

        }


        [HttpGet]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("getMessages")]
        public IActionResult GetMessages(int group_id)
        {
            var response = _groupService.GetGroupMessage(group_id);
            return Ok(response);
        }
    }
}
