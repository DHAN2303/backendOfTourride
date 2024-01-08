using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AllrideApi.Controllers.Version_1.TomtomApi
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HereNearBySearchController : ControllerBase
    {

        private readonly IHereNearBySearchService _hereNearBySearchService;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        public HereNearBySearchController(IHereNearBySearchService hereNearBySearchService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _hereNearBySearchService = hereNearBySearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }


        [HttpPost]
        [Route("nearBy")]
        public async Task<Object> RequestHereNearBySearch(Dictionary<string, dynamic> hereNearBySearchService)
        {
            var user_email = HttpContext.User.Claims.Last()?.Value;
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:here_nearby_limit"));
            var result = _usageTrackerService.CanUseService(user_email, service_id);

            if (result == "1")
            {
                var response = await _hereNearBySearchService.CreateHereNearBySearchService(hereNearBySearchService);
                if(response != null)
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

