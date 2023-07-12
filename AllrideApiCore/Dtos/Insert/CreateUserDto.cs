namespace DTO.Insert
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Phone { get; set; } // string olarak güncellenecek
        public string Country { get; set; }
        public string UserPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }

}
