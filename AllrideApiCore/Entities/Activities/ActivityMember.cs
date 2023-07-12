namespace AllrideApiCore.Entities.Activities
{
    public class ActivityMember:BaseEntity
    {
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public Activity Activitiy { get; set; }
    }
}
