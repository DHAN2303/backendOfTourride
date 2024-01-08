using AllrideApiCore.Dtos.Weather;
using AllrideApiService.Enums;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Weather;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Security.Claims;

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
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.TokenIsInValid, false));
            }
            
            int service_id = Convert.ToInt16(_config.GetValue<string>("ServiceId:weather_limit"));
            var result = _usageTrackerService.CanUseService(userEmail, service_id);

            if (result == "1")
            {
                var response = await _weatherService.SaveWeather(weatherReqDto);
                if (response.StatusCode == (int)ErrorEnumResponse.NewsIdNullOrEmpty)
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NewsIdNullOrEmpty, false));
                }

                else if (response.Data == null && response.Status)
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(SuccessEnumResponse.RegisterSuccessfull, false));
                   // return StatusCode(500, response.ErrorEnums);
                }

            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NoPermissionToAccessWeatherService, false));
                //return StatusCode(403, ErrorEnumResponse.NoPermissionToAccessWeatherService);
            }

        }
    }
}
