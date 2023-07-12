namespace AllrideApiCore.Entities.Users
{
    public class UserNewsReaction : BaseEntity
    {
        public int ActionType { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }

    }
}
