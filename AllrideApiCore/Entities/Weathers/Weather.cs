using NetTopologySuite.Geometries;

namespace AllrideApiCore.Entities.Weathers
{
    public class Weather : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Geometry Geoloc { get; set; }
        public double Temperature { get; set; }
        public DateTime Date { get; set; }
        public int Pressure { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
        public int Humidity { get; set; }
        public int StatusId { get; set; }
        public double Predection3h { get; set; }
        public int WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public int WindGust { get; set; }
        public int WeatherId { get; set; }
        //public string WeatherParameter { get; set; }
        public string WeatherDescription { get; set; }
        //public char NightDay { get; set; }


    }
}
