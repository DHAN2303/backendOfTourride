using NetTopologySuite.Geometries;

namespace AllrideApiCore.Dtos.RequestDto
{
    public class RouteDetailRequestDto
    {
        public Geometry Geoloc { get; set; }
        public Geometry OriginPoint { get; set; }
        
    }
}
