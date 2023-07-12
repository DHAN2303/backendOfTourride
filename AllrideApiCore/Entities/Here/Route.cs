using AllrideApiCore.Entities.Activities;
using AllrideApiCore.Entities.RoutePlanners;
using AllrideApiCore.Entities.Routes;
using AllrideApiCore.Entities.Users;
using NetTopologySuite.Geometries;

namespace AllrideApiCore.Entities.Here
{
    public class Route:BaseEntity
    {
        public int UserId { get; set; }
        public Geometry Geoloc { get; set; }
        public Geometry OriginPoint { get; set; }
        public Geometry DestinationPoint { get; set; }
        public double Duration { get; set; }
        public double Length { get; set; }
        public UserEntity User { get; set; }
        public int RouteTransportModeId { get; set; }
        public int EditorAdvice { get; set; }
        public string Public { get; set; }
        public bool IsRoutePlanner { get; set; }
        public RouteTransportMode RouteTransportMode { get; set; }
        public Geometry Waypoints { get; set; }
        public RouteInstruction RouteInstruction { get; set; }
        public RouteDetail RouteDetail { get; set; }
        public IEnumerable<RouteAltitude> RouteAltitudes { get; set; }
        public Activity Activity { get; set; }
        public RoutePlanner RoutePlanner { get; set; }   

    }
}
