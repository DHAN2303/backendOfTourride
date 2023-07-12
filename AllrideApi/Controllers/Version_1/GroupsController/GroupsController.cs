using AllrideApiCore.Dtos.Chat;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.GroupsController
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


        [HttpPost("createGroup")]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        public IActionResult CreateGroup([FromBody] GroupChat groups)
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int Admin);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            try
            {
                _posts.PostNewGroup(groups.group_name, groups.group_image, groups.group_description, groups.group_members, Admin);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupsController --> CreateGroup ERROR METHOD: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpGet("groupsMedias")]
        public IActionResult GetMedia(int GroupId)
        {
            try
            {
                var result = _groupService.GetGroupMedia(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupsController --> GetMedia ERROR METHOD: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("getGroupDetail")]
        public IActionResult GetGroupDetail(int GroupId)
        {
            try
            {
                var result = _groupService.GetGroupDetail(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> GetGroupDetail ERROR METHOD: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }
        [HttpGet("getGlobalGroups")]
        public IActionResult GetGlobalGroups(int GroupId, int Type = 1)
        {
            try
            {

                var result = _groupService.GetGlobalGroups(GroupId, Type);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> GetGlobalGroups ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpDelete]
        [Route("deleteGroup")]
        public IActionResult DeleteGroup(int GroupId)
        {

            try
            {

                var result = _groupService.DeleteGroup(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> DeleteGroup ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpDelete]
        [Route("deleteUserInGroup")]
        public IActionResult DeleteUserInGroup(int GroupId, int UserId)
        {
            try
            {

                var result = _groupService.DeleteUserInGroup(GroupId, UserId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> DeleteUserInGroup ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpGet("getGroupUser")]
        public IActionResult GetGroupUserDetail(int GroupId)
        {
            try
            {
                var result = _groupService.GetGroupUserDetail(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> GetGroupUserDetail ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("groupSearch")]
        public IActionResult SearchGroup(string userName, int GroupId)
        {
            try
            {
                var searchResult = _groupService.SearchUserGroup(userName, GroupId);
                if (searchResult.Status)
                {
                    return Ok(searchResult);
                }
                else
                {
                    return StatusCode(500, searchResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> SearchGroup ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }
        [HttpGet("getLastActivity")]
        public IActionResult GetLastActivity(int GroupId)
        {
            try
            {
                var result = _groupService.GetLastActivity(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> GetLastActivity ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        // 29 MAYIS
        [HttpGet("usersGroupList")]
        public IActionResult GetUsersGroupList()
        {

            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            //// UserId'yi kullanarak yapmak istediğiniz işlemler
            bool isUserIdTypeInt = int.TryParse(userId, out int Admin);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _groupService.GetUsersGroupList(Admin);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response.ErrorEnums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupsController --> GetUsersGroupList ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }


        //  29 - 30 - 31 MAYIS
        [HttpGet("groupsUsersSocialMediaLast3Posts")]
        public IActionResult GetGroupsUsersSocialMediaLast3Post(int groupId)
        {
            try
            {
                var response = _groupService.GetGroupsUsersSocialMediaLast3Post(groupId);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response.ErrorEnums);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupsController --> GetGroupsUsersSocialMediaLast3Post  ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.BadRequest);
            }
        }

        // 13 Haziran
        [HttpGet("getFollowersInGroup")]
        public IActionResult GetFollowersGroup(int GroupId)
        {
            try
            {

                var result = _groupService.GetFollowers(GroupId);
                if (result.Status)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  GroupsController --> GetGlobalGroups ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }


    }
}
