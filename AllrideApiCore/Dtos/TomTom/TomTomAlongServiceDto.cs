
namespace AllrideApiCore.Dtos.TomTom
{
    public class TomTomAlongServiceDto
    {
        public string email { get; set; }
        public string Latlong { get; set; }
        public Dictionary<string, dynamic> alongRouteSearchParam { get; set; }
        
    }
}
