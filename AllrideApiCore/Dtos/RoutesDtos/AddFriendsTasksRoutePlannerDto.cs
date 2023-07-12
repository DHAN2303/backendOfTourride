namespace AllrideApiCore.Dtos.RoutesDtos
{
    public class AddFriendsTasksRoutePlannerDto
    {
        public int RoutePlannerId { get; set; }
        public Dictionary<int, string> FriendsIdTasks { get; set; }
        public string Notes { get; set; }
    }
}
