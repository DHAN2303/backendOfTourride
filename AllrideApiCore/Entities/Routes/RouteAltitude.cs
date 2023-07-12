using AllrideApiCore.Entities.Here;
using NetTopologySuite.Geometries;

namespace AllrideApiCore.Entities.Routes
{
    public class RouteAltitude:BaseEntity
    {
        public int RouteId { get; set; }
        public Geometry Geoloc { get; set; }
        public Route Route { get; set; }

    }
}
