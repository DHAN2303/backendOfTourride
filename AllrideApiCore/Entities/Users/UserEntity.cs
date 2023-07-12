using AllrideApiCore.Entities.Activities;
using AllrideApiCore.Entities.Commons;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.RoutePlanners;
using AllrideApiCore.Entities.ServiceLimit;

namespace AllrideApiCore.Entities.Users
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public int FacebookId { get; set; }
        public int GoogleId { get; set; }
        public int InstagramId { get; set; }
        public int AppleId { get; set; }
        public bool VerifiedMember { get; set; }
        public int forgot_password_code { get; set; }
        public UserPassword UserPassword { get; set; } // Navigation Property
        public IEnumerable<News> News { get; set; }
        public UserDetail UserDetail { get; set; }
        public SmsVerification SmsVerification { get; set; }
        public IEnumerable<Route> Routes { get; set; }
        public IEnumerable<UserNewsReaction> UserNewsReactions { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ResfreshTokenEndDate { get; set; }
        public int user_type { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool ActiveUser { get; set; }
        public Activity act { get; set; }
        public IEnumerable<ServiceUsage> ServiceUsages { get; set; }
        public IEnumerable<RoutePlanner> RoutePlanners{ get; set; }
        public MemberShip MemberShip { get; set; }
        public IEnumerable<UserBlock> BlockingUserBlocks { get; set; }
        public IEnumerable<UserBlock> BlockedUserBlocks { get; set; }
        public IEnumerable<UserInvites> InvitingUser { get; set; }
        public IEnumerable<UserInvites> InvitedUser { get; set; }

    }
}
