using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Compress;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.ClubsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubSettingsController : ControllerBase
    {
        private readonly IClubSettingsService _clubSettingsService;
        private readonly IPosts _posts;
        private readonly ILogger<ClubSettingsController> _logger;
        public ClubSettingsController(IClubSettingsService clubSettingsService, ILogger<ClubSettingsController> logger, IPosts posts)
        {
            _clubSettingsService = clubSettingsService;
            _logger = logger;
            _posts = posts;
        }


        // 30 MAYIS
        [HttpPut("clubUpdateBackgrounCoverImage")]
        public async Task<IActionResult> UpdateBackgroundCoverPhoto(GroupUpdateChangePhotoDto changePhoto)
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

            var file = changePhoto.File;
            GroupImageProfileOrBackgroundCompress imageCompress = new GroupImageProfileOrBackgroundCompress(_posts);

            var extens = Path.GetExtension(file.FileName);

            try
            {
                string savedPath = "";
                if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressGroupImage(changePhoto, 25);

                // Service git ve bu pathi kaydet 
                var response = _clubSettingsService.UpdateBackgroundCoverImage(changePhoto.GroupId, UserId, savedPath);

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
                _logger.LogError(ex.Message + " ClubSettingsController  -->  UpdateBackgroundCoverPhoto  METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        // 30 MAYIS
        [HttpPatch("groupsUpdateProfileImage")]
        public async Task<IActionResult> UpdateProfileImagePhoto(GroupUpdateChangePhotoDto changePhoto)
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
                var response = _clubSettingsService.UpdateProfileImage(changePhoto.GroupId, UserId, savedPath);

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
                _logger.LogError("CLUB SETTINGS CONTROLLER UpdateProfileImagePhoto METHOS LOG ERROR:  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.BadRequest);
            }
        }

        [HttpPatch("updateGroupName")]

        // 30 MAYIS
        public async Task<IActionResult> UpdateGroupName(int clubId, string newGroupName)
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
                var response =  _clubSettingsService.UpdateClubName(clubId, UserId, newGroupName);
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
                _logger.LogError("CLUB SETTINGS CONTROLLER UpdateGroupName METHOS LOG ERROR:  " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.BadRequest);
            }
        }
    }
}
