namespace AllrideApiCore.Entities.Users
{
    public class MemberShip:BaseEntity
    {
        public int UserId { get; set; }
        public string MemberShipType { get; set; }
        public UserEntity User { get; set; }

    }
}
