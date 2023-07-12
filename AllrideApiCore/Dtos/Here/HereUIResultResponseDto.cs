using HERE.FlexiblePolyline;

namespace AllrideApiCore.Dtos.Here
{
    public class HereDirectRequestResponseDto
    {
        public List<List<LatLngZ>> polyline { get; set; }
        public int[] duration { get; set; }
        public int[] length { get; set; }

    }
}
