using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.TomtomApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;

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
            var user_email = HttpContext.User.Claims.Last()?.Value;
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
            var result = _usageTrackerService.CanUseService(user_email, service_id);

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
