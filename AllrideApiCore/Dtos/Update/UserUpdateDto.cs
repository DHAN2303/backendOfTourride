using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Dtos.Update
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public int VerifiedMember { get; set; }
        public UserPassword UserPassword { get; set; } // Navigation Property
        public UserDetail UserDetail { get; set; }
        public SmsVerification SmsVerification { get; set; }

    }
}
