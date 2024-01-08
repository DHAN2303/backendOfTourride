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
        public HereRouteController(IHereRoutingService routeCalculation, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _routeCalculation = routeCalculation;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }

        /*
         * Model Binding yaparken Get işleminde parametreleri queryden okuyacağım için direkt string olarak alıp parçalayabilirim
         * Diğer türlü verileri post metoduyla alıp bind ı dicitonary ile gerçekleştirebilirim
         */
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dictionary<string, string> parameters)
        {
            var user_email = parameters["email"].ToString();
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_routing_limit"));
            var result = _usageTrackerService.CanUseService(user_email, service_id);

            if (result == "1")
            {
                var response = await _routeCalculation.SendRequestHere(parameters);
                return Ok(CustomResponse<object>.Success(response, true, true));
            }
            else
            {
                if (result != "0")
                {
                    return Ok(CustomResponse<object>.Success(result, true, false));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.LimitExpired, false, false));
                }

            }

        }
       
    }
}
