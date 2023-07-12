
using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.Select;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AllrideApi.Controllers.Version_1
{
    [Authorize]   
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;
        public NewsController(INewsService newsService,ILogger<NewsController> logger)
        {
            _newsService= newsService;
            _logger= logger;
        }

        [HttpGet("getNews")]
        public IActionResult GetNews([FromQuery] NewsRequestDto news)
        {
            var uId = HttpContext.User.Claims.First()?.Value;

            if (string.IsNullOrEmpty(uId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(uId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            try
            {
                var response = _newsService.GetNews(news);
                if (response.Status == false)
                {
                    if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                        return BadRequest(response);
                    return StatusCode(500, response);
                }
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " NewsController  -->  GetNews METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        [HttpGet("getAllNews")]
        public IActionResult Get()
        {
            var uId = HttpContext.User.Claims.First()?.Value;

            if (string.IsNullOrEmpty(uId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(uId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            try
            {
                var response = _newsService.GetAllNews();
                if (response.Status == false)
                {
                    //if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                    //    return BadRequest(response.ErrorEnums);
                    //return StatusCode(406, response.ErrorEnums); // ????

                    return StatusCode(500, response);
                }
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " NewsController  -->  Get (GetAllNews) METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        [HttpGet("getLastNews")]
        public IActionResult GetLastNews()
        {
            var uId = HttpContext.User.Claims.First()?.Value;

            if (string.IsNullOrEmpty(uId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(uId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            try
            {
                var response = _newsService.GetLast2News();
                if (response.Status == false)
                {
                    //if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                    //    return BadRequest(response.ErrorEnums);
                    //return StatusCode(406, response.ErrorEnums); // ????

                    return StatusCode(500, response);
                }
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " NewsController  -->  GetLastNews METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        [HttpGet]
        [Route("getNewsDetail")]
        public IActionResult GetNewsDetail([FromQuery] NewsRequestDto news)
        {

            try
            {
                var response = _newsService.GetNewsDetail(news);
                if (response.Status == false)
                {
                    //if (response.ErrorEnums.Contains(ErrorEnumResponse.NewsIsNotRegister))
                    //    return BadRequest(response.ErrorEnums);
                    //return StatusCode(406, response.ErrorEnums);

                    return StatusCode(500, response);
                }
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " NewsController  -->  getNewsDetail METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }


        [HttpPost]
        [Route("saveNewsReaction")]
        public IActionResult Post(CreateActionTypeNewsDto createReactionTypeNewsDto)
        {      
            try
            {
                // Gelen news ıd nin reaction type ına göre  beğenme ve beğenmeme sayısını arıtırıcaz.
                var userId = HttpContext.User.Claims.First()?.Value;
                if (userId.IsNullOrEmpty())
                {
                    return Unauthorized();
                }
                bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

                if (isUserIdTypeInt == false)
                {
                    return Unauthorized();
                }

                var response = _newsService.PostReaction(createReactionTypeNewsDto, UserId);
                if (response.Status == false || response == null)
                {
                    return StatusCode(500, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " NewsController  -->  Get METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

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