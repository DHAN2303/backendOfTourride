using AllrideApiCore.Entities.Commons;
using AllrideApiCore.Entities.Here;

namespace AllrideApiCore.Entities.Users
{
    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int FacebookId { get; set; }
        public int GoogleId { get; set; }
        public int InstagramId { get; set; }
        public int AppleId { get; set; }
        public int VerifiedMember { get; set; }
        public UserPassword UserPassword { get; set; } // Navigation Property
        public ICollection<News> News { get; set; }
        public UserDetail UserDetail { get; set; }
        public SmsVerification SmsVerification { get; set; }
        public Route Route { get; set; }
        public ICollection<UserNewsReaction> UserNewsReactions { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ResfreshTokenEndDate { get; set; }
        public int user_type { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool ActiveUser { get; set; }
        public ICollection<UserBlock> BlockingUserBlocks { get; set; }
        public ICollection<UserBlock> BlockedUserBlocks { get; set; }
        public ICollection<UserInvites> InvitingUser { get; set; }
        public ICollection<UserInvites> InvitedUser { get; set; }
    }
}
