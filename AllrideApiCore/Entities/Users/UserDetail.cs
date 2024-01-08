
namespace AllrideApiCore.Entities.Users
{
    public class UserDetail : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; } // string olarak güncellenecek
        public string Country { get; set; }
        public string Language { get; set; }
        public string PpPath { get; set; }
        public string VehicleType { get; set; }
        public bool status { get; set; } // false: offline || true: online
        public UserEntity User { get; set; }

    }

}
