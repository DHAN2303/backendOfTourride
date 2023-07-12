namespace AllrideApiCore.Entities.Here
{
    public class RouteInstruction:BaseEntity
    {
        public int RouteId { get; set; }
        public string Instruction { get; set; }
        public int Offset { get; set; }
        public string Language { get; set; }
        public Route Route { get; set; }


    }
}
