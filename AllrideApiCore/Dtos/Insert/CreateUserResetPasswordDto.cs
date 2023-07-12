namespace AllrideApiCore.Dtos.Insert
{
    public class CreateUserResetPasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
