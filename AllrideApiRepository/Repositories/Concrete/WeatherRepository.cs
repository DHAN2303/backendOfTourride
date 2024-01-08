using AllrideApiCore.Entities.Weathers;  
using AllrideApiRepository.Repositories.Abstract;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AllrideApiDbContext _context;
        private readonly ILogger<WeatherRepository> _logger;

        public WeatherRepository(AllrideApiDbContext context, ILogger<WeatherRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool Add(Weather weather)
        {
            try
            {
                _context.Add(weather);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " "+ ex.InnerException);
                // handle the exception
                return false;
            }
        }

        public List<Weather> Get(double lat, double lon)
        {
            return  _context.weather
                .Where(w => w.Latitude >= lat - 0.5 && w.Latitude <= lat + 0.5 && w.Longitude >= lon - 0.5 && w.Longitude <= lon + 0.5)
                .ToList();
        }

        public Weather GetFirstWeather(DateTime Date)
        {
            return _context.weather.FirstOrDefault(w => w.Date >= Date);
        }

        public ICollection<Weather> GetWeathers()
        {
            return _context.weather.OrderBy(p => p.Id).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
