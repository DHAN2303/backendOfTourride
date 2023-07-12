using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.HereApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.Here
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HereRouteController : ControllerBase
    {
        private readonly IHereRoutingService _routeCalculation;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        private readonly ILogger<HereRouteController> _logger;
        public HereRouteController(ILogger<HereRouteController> logger, IHereRoutingService routeCalculation, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _routeCalculation = routeCalculation;
            _usageTrackerService = usageTrackerService;
            _config = config;
            _logger= logger;
        }

        /*
         * Model Binding yaparken Get işleminde parametreleri queryden okuyacağım için direkt string olarak alıp parçalayabilirim
         * Diğer türlü verileri post metoduyla alıp bind ı dicitonary ile gerçekleştirebilirim
         */
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> parameters)
        {
           // var uId = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)?.Value; 
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

                var response = await _routeCalculation.SendRequestHere(parameters,UserId);
                if (response.Status)
                    return Ok(response);
                else
                    return StatusCode(500, response);
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

        [HttpPost("HereRequestForLiveRoute")]

        public async Task<IActionResult> HereRequestForLiveRoute([FromBody] Dictionary<string, string> parameters) 
        {
            // var uId = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier)?.Value; 
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
                // AŞAĞIDAKİ YORUM SATIRINI KALDIRMAYI UNUTMA
                //int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
                //var result = _usageTrackerService.CanUseService(UserId, service_id);

                var response = await _routeCalculation.HereRequestForLiveRoute(parameters);
                if (response.Status)
                    return Ok(response.Data);
                else
                    return StatusCode(500, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " HereRouteController  -->  Post METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

    }
}
