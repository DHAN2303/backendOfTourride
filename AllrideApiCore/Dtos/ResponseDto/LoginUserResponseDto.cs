
namespace AllrideApiCore.Dtos.ResponseDtos
{
    public class LoginUserResponseDto
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; } // string olarak güncellenecek
        public string Country { get; set; }
        public string Language { get; set; }
        public string PpPath { get; set; }
        public string Email { get; set; }

    }
}
