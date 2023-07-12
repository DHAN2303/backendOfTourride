using AllrideApiCore.Dtos.TomTom;

namespace AllrideApiCore.Dtos
{

    public class TomTomNearbySearchResult
    {
        public NearSummary summary { get; set; }
        public NearResult[] results { get; set; }
    }

    public class NearSummary
    {
        public string queryType { get; set; }
        public int queryTime { get; set; }
        public int numResults { get; set; }
        public int offset { get; set; }
        public int totalResults { get; set; }
        public int fuzzyLevel { get; set; }
        public NearGeoBias geoBias { get; set; }
    }

    public class NearGeoBias
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class NearResult
    {
        public string type { get; set; }
        public string id { get; set; }
        public double score { get; set; }
        public double dist { get; set; }
        public string info { get; set; }
        public NearPoi poi { get; set; }
        public NearAddress address { get; set; }
        public NearPosition position { get; set; }
        public NearViewport viewport { get; set; }
        public NearEntryPoint[] entryPoints { get; set; }
    }

    public class NearPoi
    {
        public string name { get; set; }
        public string phone { get; set; }
        public NearCategory[] categorySet { get; set; }
        public string url { get; set; }
        public string[] categories { get; set; }
        public AlongClassification[] classifications { get; set; }
    }

    public class NearCategory
    {
        public int id { get; set; }
    }

    public class NearClassification
    {
        public string code { get; set; }
        public NearName[] names { get; set; }
    }

    public class NearName
    {
        public string nameLocale { get; set; }
        public string name { get; set; }
    }

    public class NearAddress
    {
        public string streetNumber { get; set; }
        public string streetName { get; set; }
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

    public class NearPosition
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class NearViewport
    {
        public NearTopLeftPoint topLeftPoint { get; set; }
        public NearBtmRightPoint btmRightPoint { get; set; }
    }

    public class NearTopLeftPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class NearBtmRightPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class NearEntryPoint
    {
        public string type { get; set; }
        public NearPosition position { get; set; }
    }
}
