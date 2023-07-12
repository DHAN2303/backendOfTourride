namespace AllrideApiCore.Entities.Users
{
    public class UserUpdate
    {
        public int UserId { get; set; }
        public string email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Phone { get; set; } // string olarak güncellenecek
        public string Country { get; set; }
        public string Language { get; set; }
        public string PpPath { get; set; }
    }
}
