
namespace AllrideApiCore.Entities.TomTom
{
    public class AlongRouteSearchRepository
    {

    }


    //ekleyen: ömer
    public class AlongRoute
    {
        public List<AlongPoint> points { get; set; }
    }

    public class AlongPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    //
}
