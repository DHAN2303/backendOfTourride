using AllrideApiCore.Entities.RoutePlanners;

namespace AllrideApiCore.Dtos.ResponseDto.RoutePlannerResponseDto
{
    public class UserHaveRoutePlannerResponseDto
    {
        public int RouteId { get; set; }
        public string RoutePlannerTitle { get; set; }
        public string RouteName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }  // Girilen tarihin üzerine 4 gün eklemek istiyorum
        public string ColorCodeHex { get; set; }
        public TimeSpan RouteAlertTime { get; set; }
        public List<UsersInRoutePlanning> UsersInRoutePlan { get; set; }
       // public int SocialMediaFollow { get; set; }

    }
}
