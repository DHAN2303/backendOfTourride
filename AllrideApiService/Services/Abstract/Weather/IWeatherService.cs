using AllrideApiCore.Dtos.Weather;
using AllrideApiCore.Entities;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Weather
{
    public interface IWeatherService
    {
        //public Task<CustomResponse<Weather>> SaveWeather(double lat, double lon);
        public Task<CustomResponse<WeatherResponseDto>> SaveWeather(WeatherRequestDto weatherReqDto);
    }
}
