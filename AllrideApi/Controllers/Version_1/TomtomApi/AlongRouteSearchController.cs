using AllrideApiCore.Dtos.TomTom;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using AllrideApiService.Services.Concrete.HereApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public AlongRouteSearchController(IAlongRouteSearchService alongRouteSearchService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {


            _alongRouteSearchService = alongRouteSearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }

        [HttpPost]
        [Route("alongRoute")]
        public async Task<Object> RequestAlongRouteSearch(string Latlong, Dictionary<string, dynamic> alongRouteSearchParam)
        {
            var user_email = HttpContext.User.Claims.Last()?.Value;
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:tomtom_along_limit"));
            var result = _usageTrackerService.CanUseService(user_email, service_id);

            if (result == "1")
            {
                var response = await _alongRouteSearchService.CreateAlongRouteSearchService(Latlong, alongRouteSearchParam);
                if (response != null)
                {
                    return Ok(CustomResponse<object>.Success(response, true, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(response, false, true));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(null, false, false));
            }

        }
    }
}
/*
 * Bu service GET yaparsam bütün parametreler url den gelir
 * post yaparsam parametreleri body den 
 * latlongu urlden alabilirim 
 */

