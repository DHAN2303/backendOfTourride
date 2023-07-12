using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities
{
    public class SmsVerification:BaseEntity
    {
        public int UserId { get; protected set; }
        public UserEntity User { get; protected set; }
        public int Code { get; protected set; }
    }
}
