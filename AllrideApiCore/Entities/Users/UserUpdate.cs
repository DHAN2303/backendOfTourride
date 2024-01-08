namespace AllrideApiCore.Entities.Users
{
    public class UserUpdate
    {
        //public int Id { get; set; }
        //public string Email { get; set; }
        //public int VerifiedMember { get; set; }
        //public UserPassword UserPassword { get; set; } // Navigation Property
        //public UserDetail UserDetail { get; set; }
        //public SmsVerification SmsVerification { get; set; }
        //public DateTime UpdatedDate { get; set; }

        public int UserId { get; set; }
        public string email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; } // string olarak güncellenecek
        public string Country { get; set; }
        public string Language { get; set; }
        public string PpPath { get; set; }
    }
}
