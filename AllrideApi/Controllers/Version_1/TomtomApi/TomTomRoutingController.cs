using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.TomtomApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace AllrideApi.Controllers.Version_1.TomtomApi
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TomTomRoutingController : ControllerBase
    {
        private readonly ITomtomRoutingService _routeService;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        public TomTomRoutingController(ITomtomRoutingService routeService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _routeService = routeService;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }
        [NonAction]
        [HttpPost]
        public async Task<IActionResult> PostRoute(Dictionary<string, string> parameters)
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
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
            var result = _usageTrackerService.CanUseService(UserId, service_id);

            if (result == "1")
            {
                var response = await _routeService.CreateRouteCalculatePost(parameters);
                return Ok(CustomResponse<object>.Success(response, true, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.LimitExpired, false, false));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoute([FromQuery] string Latlong, [FromQuery] Dictionary<string, string> parameters)
        {
            var response = await _routeService.CreateRouteCalculateGet(Latlong, new());
            return Ok(CustomResponse<object>.Success(response, true));
        }
    }
}
