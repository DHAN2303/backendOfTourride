using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.ClubsInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AllrideApi.Controllers.Version_1.ClubsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;
        private readonly IPosts _posts;
        private readonly ILogger<ClubController> _logger;
        public ClubController(IClubService clubService, ILogger<ClubController> logger, IPosts posts)
        {
            _clubService = clubService;
            _logger = logger;
            _posts = posts;
        }


        [HttpPost]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("createClub")]
        public IActionResult Club([FromBody] ClubRequestDto clubsRequestDto)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (userId.IsNullOrEmpty())
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
                _posts.CreateNewClub(clubsRequestDto, Admin);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ClubController  -->  CreateClubController METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("clubsMedias")]
        public IActionResult getMedia(int ClubId)
        {
            try
            {
                var result = _clubService.GetClubMedia(ClubId);
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
                _logger.LogError(ex.Message + "  ClubController  -->   getMedia  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        [HttpGet("getClubDetail")]
        public IActionResult GetClubDetail(int ClubId)
        {
            try
            {
                var result = _clubService.GetClubDetail(ClubId);
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
                _logger.LogError(ex.Message + "  ClubController  -->  GetClubDetail  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("getGlobalClubs")]
        public IActionResult GetGlobalClubs(int ClubId, int Type = 1)
        {
            try
            {
                var result = _clubService.GetGlobalClubs(ClubId, Type);
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
                _logger.LogError(ex.Message + "  ClubController  -->   GetGlobalClubs  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpDelete]
        [Route("deleteClub")]
        public IActionResult DeleteClub(int ClubId)
        {
            try
            {
                var result = _clubService.DeleteClub(ClubId);
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
                _logger.LogError(ex.Message + "   ClubController  -->   DeleteClub  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpDelete]
        [Route("deleteUserInClub")]
        public IActionResult DeleteUserInClub(int ClubId)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (userId.IsNullOrEmpty())
            {
                return Unauthorized();
            }
            //// UserId'yi kullanarak yapmak istediğiniz işlemler
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var result = _clubService.DeleteUserInClub(ClubId, UserId);
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
                _logger.LogError(ex.Message + "  ClubController  -->   DeleteClub  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpGet("getClubUser")]
        public IActionResult GetClubUserDetail(int ClubId)
        {
            try
            {
                var result = _clubService.GetClubUserDetail(ClubId);
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
                _logger.LogError(ex.Message + "  ClubController  -->   GetClubUserDetail  METHOD ERROR:   " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("getLastActivity")]
        public IActionResult GetLastActivity(int ClubId)
        {
            try
            {

                var result = _clubService.GetLastActivity(ClubId);
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
                _logger.LogError(ex.Message + "  ClubController  -->   GetClubUserDetail  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }
        // 29 MAYIS
        [HttpGet("usersClubList")]
        public IActionResult GetUsersClubList()
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
                var response = _clubService.GetUsersClubList(Admin);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response.ErrorEnums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  ClubController  -->   GetClubUserDetail  METHOD ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }


        }

        //  29 - 30 MAYIS
        [HttpGet("clubsUsersSocialMediaLast3Posts")]
        public IActionResult GetClubsUsersSocialMediaLast3Post(int clubId)
        {
            try
            {
                var response = _clubService.GetClubsUsersSocialMediaLast3Post(clubId);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response.ErrorEnums);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " Clubs Controller GetGroupsUsersSocialMediaLast3Post Method LOG ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpGet("GetClubAdminNumber")]
        public IActionResult GetClubAdminNumber(int clubId)
        {
            try
            {
                var response = _clubService.GetClubAdminNumber(clubId);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response.ErrorEnums);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " Clubs Controller GetClubAdminNumber Method LOG ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        // 13 Haziran
        [HttpGet("getFollowersInClub")]
        public IActionResult GetFollowersGroup(int ClubId)
        {
            try
            {

                var result = _clubService.GetFollowers(ClubId);
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
                _logger.LogError(ex.Message + "  ClubController --> GetGlobalGroups ERROR METHOD:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
    }
}
