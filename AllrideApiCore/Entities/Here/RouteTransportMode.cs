namespace AllrideApiCore.Entities.Here
{
    public class RouteTransportMode
    {        
        public int Id { get; set; }
        public string Mode { get; set; }
        public IEnumerable<Route> Route { get; set; }
    }
}


// Mode List

/*
 * pedestrian
 * bicycle
 * scooter
 * taxi
 * bus
 * privateBus
 * car
 * truck 
 */

//public int Type { get; set; }
