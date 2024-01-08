using AllrideApi.Controllers.Version_1.Chat.Group;
using AllrideApiChat.Functions.Compress;
using AllrideApiCore.Dtos.Chat;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AllrideApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploaderController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IPosts _posts;

        public UploaderController(IPosts posts, ILogger<GroupsController> logger)
        {
            _posts = posts;
            _logger = logger;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]//100 mb limit
        [Route("UploadFile")]

        public async Task<IActionResult> UploadFile([FromForm] FileModel fileModel)
        {
            VideoCompress videoCompress = new VideoCompress(_posts);
            ImageCompress imageCompress = new ImageCompress(_posts);
            
            var extens = Path.GetExtension(fileModel.file.FileName);
            string savedPath = "";
            
            try
            {
                if (extens == ".mp4" || extens==".avi" || extens == ".mov" || extens == ".m4v" || extens == ".3gp")
                   savedPath = videoCompress.CompressVideo(fileModel, 0.5);
                else if (extens == ".jpg" || extens == ".png" || extens == ".tiff" || extens == ".bmp" || extens == ".ico")
                    savedPath = imageCompress.CompressImage(fileModel, 25);

                return Ok(CustomResponse<object>.Success(savedPath, true));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException.ToString());
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }




    }
}
