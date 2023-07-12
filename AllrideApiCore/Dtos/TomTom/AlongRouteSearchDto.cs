
namespace AllrideApiCore.Dtos.TomTom
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AlongAddress
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
        public string municipalitySubdivision { get; set; }
        public string municipality { get; set; }
        public string countrySecondarySubdivision { get; set; }
        public string countrySubdivision { get; set; }
        public string countrySubdivisionName { get; set; }
        public string postalCode { get; set; }
        public string extendedPostalCode { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public string countryCodeISO3 { get; set; }
        public string freeformAddress { get; set; }
        public string localName { get; set; }
    }

    public class AlongBtmRightPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class AlongCategorySet
    {
        public int id { get; set; }
    }

    public class AlongClassification
    {
        public string code { get; set; }
        public List<AlongName> names { get; set; }
    }

    public class AlongEntryPoint
    {
        public string type { get; set; }
        public AlongPosition position { get; set; }
    }

    public class AlongName
    {
        public string nameLocale { get; set; }
        public string name { get; set; }
    }

    public class AlongPoi
    {
        public string name { get; set; }
        public string phone { get; set; }
        public List<AlongCategorySet> categorySet { get; set; }
        public string url { get; set; }
        public List<string> categories { get; set; }
        public List<AlongClassification> classifications { get; set; }
    }

    public class AlongPosition
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class AlongResult
    {
        public string type { get; set; }
        public string id { get; set; }
        public double score { get; set; }
        public double dist { get; set; }
        public string query { get; set; }
        public string info { get; set; }
        public AlongPoi poi { get; set; }
        public AlongAddress address { get; set; }
        public AlongPosition position { get; set; }
        public AlongViewport viewport { get; set; }
        public List<AlongEntryPoint> entryPoints { get; set; }
        public int detourTime { get; set; }
        public int detourDistance { get; set; }
    }

    public class AlongRoot
    {
        public AlongSummary summary { get; set; }
        public List<AlongResult> results { get; set; }
    }

    public class AlongSummary
    {
        public string query { get; set; }
        public string queryType { get; set; }
        public int queryTime { get; set; }
        public int numResults { get; set; }
        public int offset { get; set; }
        public int totalResults { get; set; }
        public int fuzzyLevel { get; set; }
    }

    public class AlongTopLeftPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class AlongViewport
    {
        public AlongTopLeftPoint topLeftPoint { get; set; }
        public AlongBtmRightPoint btmRightPoint { get; set; }
    }



}
