namespace AllrideApiCore.Dtos.RequestDto.RoutePlanner
{
    public class AddFriendsTasksRoutePlannerDto
    {
        public int RoutePlannerId { get; set; }
        public Dictionary<int, List<int>> FriendsAndTasksId { get; set; }
        public string Notes { get; set; }

    }
}
