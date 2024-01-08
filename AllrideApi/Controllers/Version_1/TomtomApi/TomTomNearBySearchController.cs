using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.TomtomApi
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TomTomNearBySearchController : ControllerBase
    {

        private readonly ITomTomNearBySearchService _nearBySearchService;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;

        public TomTomNearBySearchController(ITomTomNearBySearchService nearBySearchService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _nearBySearchService = nearBySearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }


        [HttpPost]
        [Route("nearBy")]
        public async Task<Object> RequestNearBySearch( Dictionary<string, dynamic> nearBySearchParam)
        {
            var user_email = HttpContext.User.Claims.Last()?.Value;
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:here_nearby_limit"));
            var result = _usageTrackerService.CanUseService(user_email, service_id);
            if (result == "1")
            {
                var response = await _nearBySearchService.CreateNearBySearchService(nearBySearchParam);
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

