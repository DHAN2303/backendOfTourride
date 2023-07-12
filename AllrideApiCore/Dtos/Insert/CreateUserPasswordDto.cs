namespace DTO.Insert
{
    public class CreateUserPasswordDto
    {
        public CreateUserDto User { get; set; }  // Bu kez nesne olaraktan hangi User kullanıcısına ait olduğunu bilmem lazım
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
