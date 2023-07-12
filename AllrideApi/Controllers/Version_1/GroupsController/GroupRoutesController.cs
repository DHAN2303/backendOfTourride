using AllrideApi.Controllers.Version_1.Here;
using AllrideApiService.Services.Abstract.HereApi;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AllrideApiService.Response;

namespace AllrideApi.Controllers.Version_1.GroupsController
{
    [NonController]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRoutesController : ControllerBase
    {
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        private readonly ILogger<HereRouteController> _logger;
        public GroupRoutesController(ILogger<HereRouteController> logger,IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _usageTrackerService = usageTrackerService;
            _config = config;
            _logger = logger;
        }
        [HttpPost("createRouteForGroup")]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> parameters)
        { 
            var uId = HttpContext.User.Claims.First()?.Value;

            if (string.IsNullOrEmpty(uId))
            {
                return Unauthorized();
            }

            //// UserId'yi kullanarak yapmak istediğiniz işlemler
            bool isUserIdTypeInt = int.TryParse(uId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            try
            {
                //int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
                //var result = _usageTrackerService.CanUseService(UserId, service_id);

                //var response = await _routeCalculation.SendRequestHere(parameters, UserId);
                //if (response.Status)
                //    return Ok(response);
                //else
                    return StatusCode(500, "başarısız");
                //if (result == "1")
                //{
                //    var response = await _routeCalculation.SendRequestHere(parameters);
                //    return Ok(response);
                //}
                //else
                //{
                //    return StatusCode(500, ErrorEnumResponse.LimitExpired);
                //    //if (result != "0")
                //    //{
                //    //    return StatusCode(500, ErrorEnumResponse.LimitExpired);
                //    //}
                //    //else
                //    //{
                //    //    return StatusCode(500,);
                //    //}

                //}

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " HereRouteController  -->  Post METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

    }
}
