
namespace AllrideApiCore.Entities.ServiceLimit
{
    public class UserTypes
    {
       // public int id { get; set; }
        public int type { get; set; }
        public int tomtom_nearby_limit { get; set; }
        public int tomtom_along_limit { get; set; }
        public int here_nearby_limit { get; set; }
        public int tomtom_routing_limit { get; set; }
        public int weather_limit { get; set; }
        public int here_route_limit { get; set; }
    }
}
