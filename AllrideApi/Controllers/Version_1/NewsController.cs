using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.Select;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AllrideApi.Controllers.Version_1
{
    [Authorize]
   
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService= newsService;
        }

        [HttpGet]
        [Route("getNews")]
        public  IActionResult Get([FromQuery] NewsRequestDto news)
        {
            var response = _newsService.GetNews(news);
            if (response.Status == false)
            {
                if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                    return BadRequest(response.ErrorEnums);
                return StatusCode(406, response.ErrorEnums);
            }
            else
                return  Ok(response.Data);   
        }

        [HttpGet]
        [Route("getNewsDetail")]
        public IActionResult GetNewsDetail([FromQuery] NewsRequestDto news)
        {
            var response = _newsService.GetNewsDetail(news);
            if (response.Status == false)
            {
                if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                    return BadRequest(response.ErrorEnums);
                return StatusCode(406, response.ErrorEnums);
            }
            else
                return Ok(response.Data);
        }


        [HttpPost]
        [Route("saveNews")]
        public IActionResult Post(CreateActionTypeNewsDto createReactionTypeNewsDto)
        {
            // Gelen news ıd nin reaction type ına göre  beğenme ve beğenmeme sayısını arıtırıcaz.
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
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

            var response = _newsService.PostReaction(createReactionTypeNewsDto, UserId);
            if(response.Status == false || response ==null)
            {
                return StatusCode(500, response.ErrorEnums);
            }
            return Ok(response.Data);

        }


    }
}


























//[HttpGet("lastNews")]
//public IActionResult GetLastNews()
//{
//    return Ok(_newsService.GetLast2News());
//}

//[HttpPost]
//public IActionResult Post(CreateActionTypeNewsDto createReactionTypeNewsDto)
//{
//    // Gelen news ıd nin reaction type ına göre  beğenme ve beğenmeme sayısını arıtırıcaz.
//    var response = _newsService.PostReaction(createReactionTypeNewsDto);
//    return Ok(response);

//}