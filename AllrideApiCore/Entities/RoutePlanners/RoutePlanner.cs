
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities.RoutePlanners
{
    public class RoutePlanner : BaseEntity
    {
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public string RoutePlannerTitle { get; set; }
        public string RouteName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }  // Girilen tarihin üzerine 4 gün eklemek istiyorum
        public string ColorCodeHex { get; set; }
        public TimeSpan RouteAlertTime { get; set; }
        public IEnumerable<Route> Route { get; set; }
        public IEnumerable<TasksRoutePlanner> TasksRoutePlanners { get; set; } 
        public IEnumerable<UsersInRoutePlanning> UsersInRoutePlannings { get; set; } 
        public UserEntity UserEntities { get; set; }



    }
}


//public Club Club { get; set; }
//public Group Group { get; set; }