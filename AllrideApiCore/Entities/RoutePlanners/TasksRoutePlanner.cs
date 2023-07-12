namespace AllrideApiCore.Entities.RoutePlanners
{
    public class TasksRoutePlanner:BaseEntity
    {
        public int RoutePlannerId { get; set; }
        public string Tasks { get; set; }
        public RoutePlanner RoutePlanner { get; set; }

    }
}
