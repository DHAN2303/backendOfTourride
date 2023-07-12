using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AllrideApi.Controllers.Version_1.TomtomApi
{

    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlongRouteSearchController : ControllerBase
    {

        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IAlongRouteSearchService _alongRouteSearchService;
        private readonly IConfiguration _config;
        private readonly ILogger<AlongRouteSearchController> _logger;

        public AlongRouteSearchController(IAlongRouteSearchService alongRouteSearchService, IUsageTrackerService usageTrackerService, IConfiguration config, ILogger<AlongRouteSearchController> logger)
        {
            _alongRouteSearchService = alongRouteSearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        [Route("alongRoute")]
        public async Task<Object> RequestAlongRouteSearch(string Latlong, Dictionary<string, dynamic> alongRouteSearchParam)
        {

            try
            {
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
                int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_along_limit"));
                var result = _usageTrackerService.CanUseService(UserId, service_id);

                if (result == "1")
                {
                    var response = await _alongRouteSearchService.CreateAlongRouteSearchService(Latlong, alongRouteSearchParam);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(500, response);
                    }
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(null, false, false));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" Along Route Search Controller Api ERROR:  " + ex.Message);
                return StatusCode(500,ErrorEnumResponse.ApiServiceFail);
            }

        }
    }
}
