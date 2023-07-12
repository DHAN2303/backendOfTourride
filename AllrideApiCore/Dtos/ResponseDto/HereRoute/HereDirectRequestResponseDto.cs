namespace AllrideApiCore.Dtos.ResponseDto.HereRoute
{
    public class HereDirectApiResponse
    {   
        public Root2 Root { get; set; }
        public List<Route2> Route { get; set; }
        public List<Section2> Sect { get; set; }
        public Summary2 Summary { get; set; }
        public Transport2 Transport{ get; set; }

    }
    public class Action2
    {
        public string action { get; set; }
        public int duration { get; set; }
        public int length { get; set; }
        public string instruction { get; set; }
        public int offset { get; set; }
        public string direction { get; set; }
        public int? exit { get; set; }
        public string severity { get; set; }
    }

    public class Arrival2
    {
        public DateTime time { get; set; }
        public Place2 place { get; set; }
    }

    public class Departure2
    {
        public DateTime time { get; set; }
        public Place2 place { get; set; }
    }

    public class Location2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class OriginalLocation2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Place2
    {
        public string type { get; set; }
        public Location2 location { get; set; }
        public OriginalLocation2 originalLocation { get; set; }
        public int? waypoint { get; set; }
    }

    public class Root2
    {
        public List<Route2> routes { get; set; }
    }

    public class Route2
    {
        public string id { get; set; }
        public List<Section2> sections { get; set; }
    }

    public class Section2
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<Action2> actions { get; set; }
        public Departure2 departure { get; set; }
        public Arrival2 arrival { get; set; }
        public Summary2 summary { get; set; }
        public string polyline { get; set; }
        public string language { get; set; }
        public Transport2 transport { get; set; }
    }

    public class Summary2
    {
        public int duration { get; set; }
        public int length { get; set; }
        public int baseDuration { get; set; }
    }

    public class Transport2
    {
        public string mode { get; set; }
    }

}
