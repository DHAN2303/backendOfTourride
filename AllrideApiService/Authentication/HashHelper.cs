using System.Security.Cryptography;
using System.Text;

namespace AllrideApiService.Authentication
{
    public static class HashHelper
    {
        // Şifrelenmiş ve hashlenmiş kullanıcı şifresi oluşturma işlemi
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hashMac = new HMACSHA512();
            passwordSalt = hashMac.Key; // Burada bir private ya da public key üretiyor.
            passwordHash = hashMac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        // Şifre Hash' inin, requestten gelen şifre ile doğruluğunu kontrol etme
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }
    }
}
