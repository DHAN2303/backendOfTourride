using NetTopologySuite.Geometries;

namespace AllrideApiCore.Dtos.ResponseDto
{
    public class RouteResponseDto
    {
        public Geometry Geoloc { get; set; }
        public Geometry OriginPoint { get; set; }
        public Geometry DestinationPoint { get; set; }
        public Geometry WayPoints { get; set; }
        public double Length { get; set; }
        public double Duration { get; set; }
        public int TransportType { get; set; }
        public int EditorAdvice { get; set; }
        public string Pub { get; set; }
        public string[] PolyLine { get; set; }
    }
}
