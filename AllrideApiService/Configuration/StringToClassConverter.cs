using AllrideApiCore.Entities.Users;
using AllrideApiService.Authentication;
using AutoMapper;

namespace AllrideApiService.Configuration
{
    public class StringToClassConverter : ITypeConverter<string, UserPassword>
    {
        public UserPassword Convert(string password, UserPassword user, ResolutionContext context)
        {
            byte[] passwordHash, passwordSalt;  // Şifreleme bilgilerini tutacak için hash ve salt değişkenleri

            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            UserPassword userPassword = new UserPassword()  // Password newlendi
            {
                HashPass = passwordHash,
                SaltPass = passwordSalt
            };


            return userPassword;
        }
    }
}
