using AllrideApi.Controllers.Version_1.Here;
using AllrideApiCore.Dtos.RequestDto.GraphHopper;
using AllrideApiService.Services.Abstract.HereApi;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using AllrideApiService.Services.Abstract.GraphHoperRoundTripRouteApi;
using AllrideApiService.Response;

namespace AllrideApi.Controllers.Version_1.GraphHopper
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundTripRouteController : ControllerBase
    {
        private readonly IRoundTripRouteService _roundTripService;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        private readonly ILogger<RoundTripRouteController> _logger;
        public RoundTripRouteController(IRoundTripRouteService roundTripService, 
            IUsageTrackerService usageTrackerService, 
            IConfiguration configuration,
            ILogger<RoundTripRouteController> logger)
        {
            _roundTripService= roundTripService;
            _usageTrackerService= usageTrackerService;
            _config= configuration;
            _logger= logger;

        }

        //[HttpPost]
        //public async Task<IActionResult> MakeRoundTripRoute(CreateRoundTripRequestDto createRoundTripRequestDto)
        //{
        //    try
        //    {
        //        var uId = HttpContext.User.Claims.First()?.Value;
        //        if (string.IsNullOrEmpty(uId))
        //        {
        //            return Unauthorized();
        //        }
        //        bool isUserIdTypeInt = int.TryParse(uId, out int UserId);

        //        if (isUserIdTypeInt == false)
        //        {
        //            return Unauthorized();
        //        }

        //        //int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
        //        //var result = _usageTrackerService.CanUseService(UserId, service_id);

        //        var response = _roundTripService.GetRoundTripRoute(createRoundTripRequestDto, UserId);
        //        if (response.Status ==  false)
        //            return Ok(response);
        //        else
        //            return StatusCode(500, response);

        //        //if (result == "1")
        //        //{
        //        //    var response = await _routeCalculation.SendRequestHere(parameters);
        //        //    return Ok(response);
        //        //}
        //        //else
        //        //{
        //        //    return StatusCode(500, ErrorEnumResponse.LimitExpired);
        //        //    //if (result != "0")
        //        //    //{
        //        //    //    return StatusCode(500, ErrorEnumResponse.LimitExpired);
        //        //    //}
        //        //    //else
        //        //    //{
        //        //    //    return StatusCode(500,);
        //        //    //}

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message + " RoundTripRouteController  -->  Post METHOD  ERROR: " + ex.InnerException.ToString());
        //        return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
        //    }
        //}
    }
}
