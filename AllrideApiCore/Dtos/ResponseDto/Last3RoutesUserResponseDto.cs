using NetTopologySuite.Geometries;

namespace AllrideApiCore.Dtos.ResponseDto
{
    public class Last3RoutesUserResponseDto
    {
        public Geometry Geoloc { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }

    }
}
