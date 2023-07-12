namespace AllrideApiCore.Dtos.RoutesDtos
{
    public class RouteCalculationURL
    {
        public int MaxAlternatives { get; set; }
        public string InstructionsType { get; set; }  // coded text tagged
        public string Language { get; set; }
        public bool ComputeBestOrder { get; set; }
        public string RouteRepresentation { get; set; } // Route representation ı none olarak girince tomtom bad request hatası dönüyor.
        public bool Traffic { get; set; }
        public string RouteType { get; set; } //fastest, shortest, short, eco, thrilling 


    }
}
