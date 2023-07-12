using AllrideApiCore.Dtos.Weather;
using AllrideApiCore.Entities.Weathers;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Weather;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Point = NetTopologySuite.Geometries.Point;

namespace AllrideApiService.Services.Concrete.Weathers
{
    public class WeatherService : IWeatherService
    {

        private readonly IConfiguration _config;
        private readonly ILogger<WeatherService> _logger;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;
        public WeatherService(IConfiguration config, ILogger<WeatherService> logger,
             IWeatherRepository weatherRepository, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _weatherRepository = weatherRepository;
            _mapper = mapper;

        }

        /*
         *   NOT WEATHER SERVICE DE ÜCRETSİZ DENEME SÜRÜMÜNÜ KULLANIYORUZ DAHA SONRA BU API YE GEÇİCEZ
         * https://openweathermap.org/api/hourly-forecast
         */
        public async Task<CustomResponse<WeatherResponseDto>> SaveWeather(WeatherRequestDto weatherReqDto)
        {
            const double thresholdDistance = 50; // km
            List<ErrorEnumResponse> errors = new();
            Weather resultWeather = new();
            bool IsRegistered = false;
            WeatherResponseDto closestWeather = null;
            double closestDistance = double.MaxValue;

            WeatherResponseDto weatherResponse = new();
            try
            {
                using var client = new HttpClient();

                var existingWeather = _weatherRepository.Get(weatherReqDto.Latitude, weatherReqDto.Longitude);
                //Verilen konumun veritabanından gelen konumlardan birine yakın olup olmadığını kontrol ediyor
                foreach (var weather in existingWeather)
                {
                    var distance = CalculateDistance(weatherReqDto.Latitude, weatherReqDto.Longitude, weather.Latitude, weather.Longitude);
                    if (distance < closestDistance)
                    {
                        closestWeather = _mapper.Map<WeatherResponseDto>(weather);
                        closestDistance = distance;
                    }
                }

                // Bulunan yakın bir konum varsa, veritabanından döndürün

                if (closestDistance <= thresholdDistance)
                {
                    return CustomResponse<WeatherResponseDto>.Success(closestWeather, true);
                }
                var url = _config.GetSection("OpenWeather").GetSection("WeatherBaseUrl").Value + "?";
                url += "lat=" + weatherReqDto.Latitude;
                url += "&lon=" + weatherReqDto.Longitude;
                url += "&appid=" + _config.GetSection("OpenWeather").GetSection("ApiKey").Value;
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    errors.Add(ErrorEnumResponse.WeatherServiceResponseError);
                    return CustomResponse<WeatherResponseDto>.Fail(errors, false);
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                //JsonSerializerOptions options = new()
                //{
                //    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //    MaxDepth = 64
                //};
                var metadata = JsonSerializer.Deserialize<WeatherResultDto>(responseContent); //,options

                if (metadata.Count <= 0)
                {
                    errors.Add(ErrorEnumResponse.WeatherServiceJsonNotDeserialize);
                    return CustomResponse<WeatherResponseDto>.Fail(errors, false);
                }

                foreach (var m in metadata.WeatherDataList)
                {
                    var weather = new Weather
                    {
                        Temperature = m.Main.Temperature,
                        Pressure = m.Main.Pressure,
                        SeaLevel = m.Main.SeaLevel,
                        GrndLevel = m.Main.GroundLevel,
                        Humidity = m.Main.Humidity,
                        WindSpeed = (int)m.Wind.Speed,
                        WindDeg = (int)m.Wind.Deg,
                        WindGust = (int)m.Wind.Gust,
                        Date = DateTime.Parse(m.DateTimeText),  // Tahmin edilen verilerin zamanı
                    };

                    foreach (var weatherInfo in m.WeatherInfo)
                    {
                        weather.WeatherId = weatherInfo.Id;
                        weather.WeatherDescription = weatherInfo.Description;
                    }
                    weather.Latitude = weatherReqDto.Latitude;
                    weather.Longitude = weatherReqDto.Longitude;
                    weather.Geoloc = new Point(weather.Longitude, weather.Latitude);
                    IsRegistered = _weatherRepository.Add(weather);
                }
                _weatherRepository.SaveChanges();
                if (IsRegistered)
                {
                    resultWeather = _weatherRepository.GetFirstWeather(weatherReqDto.Date);
                    if (resultWeather == null)
                    {
                        errors.Add(ErrorEnumResponse.WeatherRequestedDateHasPassed);
                        return CustomResponse<WeatherResponseDto>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.NoWeatherDataSavedInDb);
                    return CustomResponse<WeatherResponseDto>.Fail(errors, false);
                }

                weatherResponse = _mapper.Map<WeatherResponseDto>(resultWeather);

                if (weatherResponse != null)
                {
                    errors.Add(ErrorEnumResponse.BackendDidntAutoMapper);
                    return CustomResponse<WeatherResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Weather Servisi Log Error: " + ex.Message, ex);
                return CustomResponse<WeatherResponseDto>.Fail(errors, false);

            }
            return CustomResponse<WeatherResponseDto>.Success(weatherResponse, true);
        }
      
        public async Task<CustomResponse<object>> WeatherRequestForRoute(WeatherRequestDto weatherReqDto)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                using var client = new HttpClient();
                var url = _config.GetSection("OpenWeather").GetSection("WeatherBaseUrl").Value + "?";
                url += "lat=" + weatherReqDto.Latitude;
                url += "&lon=" + weatherReqDto.Longitude;
                url += "&appid=" + _config.GetSection("OpenWeather").GetSection("ApiKey").Value;
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    errors.Add(ErrorEnumResponse.WeatherServiceResponseError);
                    return CustomResponse<object>.Fail(errors, false);
                }
                var responseContent = await response.Content.ReadAsStringAsync();
                //JsonSerializerOptions options = new()
                //{
                //    NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //    MaxDepth = 64
                //};
                var metadata = JsonSerializer.Deserialize<WeatherResultDto>(responseContent); //,options

                if (metadata.Count <= 0)
                {
                    errors.Add(ErrorEnumResponse.WeatherServiceJsonNotDeserialize);
                    return CustomResponse<object>.Fail(errors, false);
                }
                return CustomResponse<object>.Success(metadata, true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Weather Servisi Log Error: " + ex.Message, ex);
                return CustomResponse<object>.Fail(errors, false);
            }

        }

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

    }
}
