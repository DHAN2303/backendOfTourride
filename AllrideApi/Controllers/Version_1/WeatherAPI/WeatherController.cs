using AllrideApiCore.Dtos.Weather;
using AllrideApiService.Enums;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Weather;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.WeatherAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IUsageTrackerService _usageTrackerService;
        private readonly IConfiguration _config;
        public WeatherController(IWeatherService weatherService, IUsageTrackerService usageTrackerService, IConfiguration config)
        {
            _weatherService= weatherService;
            _usageTrackerService = usageTrackerService;
            _config = config;
        }
        [HttpPost]
        [Route("weatherSave")]
        public async Task<IActionResult> WeatherPost(WeatherRequestDto weatherReqDto)
        {
            // var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userId = HttpContext.User.Claims.First().Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.TokenIsInValid, false));
            }

            bool isUserIdTypeInt = int.TryParse(userId, out int Admin);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:weather_limit"));
            var result = _usageTrackerService.CanUseService(Admin, service_id);

            if (result == "1")
            {
                var response = await _weatherService.SaveWeather(weatherReqDto);

                if (response.Status)
                {
                    return StatusCode(201,response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            else
            {
                //return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NoPermissionToAccessWeatherService, false));
                return StatusCode(403, ErrorEnumResponse.NoPermissionToAccessWeatherService);
            }

        }

        [HttpGet("weatherRequestForRoute")]
        public async Task<IActionResult> WeatherRequestForRoute([FromQuery] WeatherRequestDto weatherRequestDto)
        {
            var userId = HttpContext.User.Claims.First().Value;

            if (string.IsNullOrEmpty(userId))
            {
                return StatusCode(500, ErrorEnumResponse.TokenIsInValid);
            }

            bool isUserIdTypeInt = int.TryParse(userId, out int Admin);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            //int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:weather_limit"));
            //var result = _usageTrackerService.CanUseService(Admin, service_id);
            try
            {
                var response = await _weatherService.WeatherRequestForRoute(weatherRequestDto);

                if (response.Status)
                {
                    return StatusCode(201, response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            catch (Exception ex)
            {
                
                return StatusCode(403, ErrorEnumResponse.NoPermissionToAccessWeatherService);
            }

            //if (result == "1")
            //{
            //    var response = await _weatherService.SaveWeather(weatherRequestDto);

            //    if (response.Status)
            //    {
            //        return StatusCode(201, response);
            //    }
            //    else
            //    {
            //        return StatusCode(500, response);
            //    }

            //}
            //else
            //{
            //    //return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NoPermissionToAccessWeatherService, false));
            //    return StatusCode(403, ErrorEnumResponse.NoPermissionToAccessWeatherService);
            //}
        }

    }
}
