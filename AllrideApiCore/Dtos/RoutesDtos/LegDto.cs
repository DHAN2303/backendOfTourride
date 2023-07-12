namespace AllrideApiCore.Dtos.RoutesDtos
{
    public class LegDto
    {
        public int Id { get; set; }
        public List<RoutesEntities> points { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
