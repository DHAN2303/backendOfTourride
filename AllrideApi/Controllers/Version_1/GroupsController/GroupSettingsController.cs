using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Compress;
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
    public class GroupSettingsController : ControllerBase
    {
        private readonly IPosts _posts;
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupSettingsService _groupSettingsService;
        public GroupSettingsController(ILogger<GroupsController> logger, IPosts posts, IGroupSettingsService groupSettingsService)
        {
            _posts = posts;
            _logger = logger;
            _groupSettingsService = groupSettingsService;
        }
        // 28 MAYIS
        [HttpPut("updateGroupBackgroundCoverImage")]
        public async Task<IActionResult> UpdateBackgroundCoverPhoto([FromForm] GroupUpdateChangePhotoDto changePhoto)
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


            try
            {
                var file = changePhoto.File;
                GroupImageProfileOrBackgroundCompress imageCompress = new(_posts);

                var extens = Path.GetExtension(file.FileName);
                string savedPath = "";
                if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressGroupImage(changePhoto, 25);

                // Service git ve bu pathi kaydet 
                var response = _groupSettingsService.UpdateBackgroundCoverImage(changePhoto.GroupId, UserId, savedPath);

                if (response.Status)
                {
                    return Ok(response.Value);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupSettingsController  -->  UpdateBackgroundCoverPhoto METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        // 30 MAYIS
        [HttpPatch("updateGroupProfileImage")]
        public async Task<IActionResult> UpdateProfileImagePhoto([FromForm] GroupUpdateChangePhotoDto changePhoto)
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


            try
            {
                var file = changePhoto.File;
                GroupImageProfileOrBackgroundCompress imageCompress = new(_posts);

                var extens = Path.GetExtension(file.FileName);
                string savedPath = "";
                if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressGroupImage(changePhoto, 25);

                // Service git ve bu pathi kaydet 
                var response = _groupSettingsService.UpdateProfileImage(changePhoto.GroupId, UserId, savedPath);

                if (response.Status)
                {
                    return Ok(response.Value);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("GROUP SETTINGS CONTROLLER UpdateProfileImagePhoto METHOS LOG ERROR:  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.BadRequest);
            }
        }

        [HttpPatch("updateGroupName")]

        public async Task<IActionResult> UpdateGroupName(int groupId, string newGroupName)
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

            try
            {
                var response = _groupSettingsService.UpdateGroupName(groupId, UserId, newGroupName);
                if (response.Status)
                {
                    return Ok(response.Value);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GROUP SETTINGS CONTROLLER UpdateGroupName METHOS LOG ERROR:  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

    }
}
