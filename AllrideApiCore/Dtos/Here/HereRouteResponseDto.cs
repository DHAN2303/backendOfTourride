// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

public class Action  // Instructiona kaydet
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

public class Arrival
{
    public DateTime time { get; set; }
    public Place place { get; set; }
}

public class CurrentRoad
{
    public List<Name> name { get; set; }
}

public class Departure
{
    public DateTime time { get; set; }
    public Place place { get; set; }
}

// Kaza bilgileri
public class Incident
{
    public string type { get; set; }
    public string criticality { get; set; }
    public DateTime validFrom { get; set; }
    public DateTime validUntil { get; set; }
    public string description { get; set; }
    public string id { get; set; }
}

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
    public double elv { get; set; }
}

public class Name
{
    public string value { get; set; }
    public string language { get; set; }
}

public class NextRoad
{
    public List<Name> name { get; set; }
}

public class OriginalLocation
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Place
{
    public string type { get; set; }
    public Location location { get; set; }
    public OriginalLocation originalLocation { get; set; }
    public int? waypoint { get; set; }
}

public class Root
{
    public List<RouteDto> routes { get; set; }
}

public class RouteDto
{
    public string id { get; set; }
    public List<Section> sections { get; set; }
}

public class Section
{
    public string id { get; set; }
    public string type { get; set; }
    public List<Action> actions { get; set; }
    public List<TurnByTurnAction> turnByTurnActions { get; set; }
    public Departure departure { get; set; }
    public Arrival arrival { get; set; }
    public Summary summary { get; set; }
    public TravelSummary travelSummary { get; set; }
    public string polyline { get; set; }
    public string language { get; set; }
    public Transport transport { get; set; }
    public List<Incident> incidents { get; set; }
}

public class Summary
{
    public int duration { get; set; }
    public int length { get; set; }
    public int baseDuration { get; set; }
}

public class Transport
{
    public string mode { get; set; }
}

public class TravelSummary
{
    public int duration { get; set; }
    public int length { get; set; }
    public int baseDuration { get; set; }
}

public class TurnByTurnAction  // Instruction Detaila kaydet, 
{
    public string action { get; set; }
    public int duration { get; set; }
    public int length { get; set; }
    public int offset { get; set; }
    public NextRoad nextRoad { get; set; }
    public string direction { get; set; }
    public CurrentRoad currentRoad { get; set; }
    public int? exit { get; set; }
    public string severity { get; set; }
}

