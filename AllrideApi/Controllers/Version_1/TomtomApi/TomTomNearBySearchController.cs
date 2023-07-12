
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        private readonly ILogger<TomTomNearBySearchController> _logger;

        public TomTomNearBySearchController(ILogger<TomTomNearBySearchController> logger, ITomTomNearBySearchService nearBySearchService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _nearBySearchService = nearBySearchService;
            _usageTrackerService = usageTrackerService;
            _config = config;
            _logger= logger;
        }


        [HttpPost]
        [Route("nearBy")]
        public async Task<Object> RequestNearBySearch( Dictionary<string, dynamic> nearBySearchParam)
        {
            try
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

                int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:here_nearby_limit"));
                var result = _usageTrackerService.CanUseService(UserId, service_id);

                if (result == "1")
                {
                    var response = await _nearBySearchService.CreateNearBySearchService(nearBySearchParam);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return StatusCode(500,response);
                    }
                }
                else
                {
                    return  StatusCode(500, ErrorEnumResponse.LimitExpired);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ClubController  -->  CreateClubController METHOD  ERROR: " + ex.InnerException.ToString());
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

