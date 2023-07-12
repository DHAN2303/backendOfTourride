using NetTopologySuite.Geometries;

namespace AllrideApiCore.Entities.Routes
{
    public class RoutePoi:BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Geometry Geoloc { get; set; }
        public double Distance { get; set; }
        public double ExcursionDistance { get; set; }
        public string Address { get; set; }
        public int  ApiType { get; set; }
        public string[] Categories { get; set; }

    }
}
