using AllrideApiCore.Entities.Weathers;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IWeatherRepository
    {
        public List<Weather> Get(double lat, double lon);
        public ICollection<Weather> GetWeathers();
        public bool Add(Weather weather);
        public Weather GetFirstWeather(DateTime Date); 
        public void SaveChanges();
    }
}
