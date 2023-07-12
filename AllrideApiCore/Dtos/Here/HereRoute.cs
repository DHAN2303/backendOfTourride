namespace AllrideApiCore.Dtos.Here
{
    public class HereRoute
    {
        //transportMode=car, origin="47.584247,11.055542",  destination="47.589299,11.060172" 
        public string TransportMode { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        //public HereRoute(string destination, string origin, string TransporterMode)
        //{
        //    this.Destination = destination;
        //    this.Origin = origin;
        //    this.TransportMode = TransporterMode;
        //}
    }
}
