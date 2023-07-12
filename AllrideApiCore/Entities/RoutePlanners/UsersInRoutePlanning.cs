using AllrideApiCore.Entities.SocialMedia;

namespace AllrideApiCore.Entities.RoutePlanners
{
    public class UsersInRoutePlanning:BaseEntity
    {
        public int RoutePlannerId { get; set; }
        public int SocialMediaFollower { get; set; }
        public List<int> TasksId { get; set; }
        public SocialMediaFollow SocialMediaFollow { get; set; }
        public RoutePlanner RoutePlanner { get; set; }

    }
}
