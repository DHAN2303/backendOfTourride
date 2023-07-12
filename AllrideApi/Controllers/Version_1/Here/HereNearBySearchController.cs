
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        private readonly ILogger<HereNearBySearchController> _logger;
        public HereNearBySearchController(IHereNearBySearchService hereNearBySearchService, IUsageTrackerService usageTrackerService, IConfiguration config, ILogger<HereNearBySearchController> logger)
        {
            _hereNearBySearchService = hereNearBySearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
            _logger = logger;
        }


        [HttpPost]
        [Route("nearBy")]
        public async Task<Object> RequestHereNearBySearch(Dictionary<string, dynamic> hereNearBySearchService)
        {
            //var user_email = HttpContext.User.Claims.Last()?.Value;

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

            try
            {
                int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:here_nearby_limit"));
                var result = _usageTrackerService.CanUseService(UserId, service_id);

                if (result == "1")
                {
                    var response = await _hereNearBySearchService.CreateHereNearBySearchService(hereNearBySearchService);
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
                _logger.LogError(ex.Message + " HereNearBySearchController --> RequestHereNearBySearch ERROR:  " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
    }
}
/*
 * Bu service GET yaparsam b�t�n parametreler url den gelir
 * post yaparsam parametreleri body den 
 * latlongu urlden alabilirim 
 */

