//using AllrideApiCore.Entities;
//using AllrideApiService.Abstract;
//using AllrideApiService.Authentication;
//using AllrideApiService.Response;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace AllrideApiService.Concrete
//{
//    public class TokenHandler : ITokenHandler
//    {
//        private readonly IConfiguration _configuration;

//        public TokenHandler(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public BaseResponse CreateAccessToken(User user, UserDetail userDetail)
//        {
//            AccessToken token = new AccessToken();
//            // tokenı oluşturabilmek için gerekli key oluşturuldu
//            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
//            // oluşan keyi şifreliyor. Aslında şifrelenmiş bir kimlik oluşturuyor.
//            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
//            token.ExpireDate = DateTime.Now.AddMinutes(1);

//            // Claim bazlı kimlik doğrulama, Payload alanı
//            var claims = new Claim[]
//            {
//                        new Claim(ClaimTypes.Email, user.Email),
//                        new Claim(ClaimTypes.GivenName, userDetail.Name),
//            };

//            //yukarıdaki bilgileri ve configdeki kullanarak token oluşturyoruz.
//            //Tokenın tipi JwtSecurity. Token ayarlarını oluşturduk.
//            //Token oluştuma şeklini belirledik tokenımız hazır.
//            //Payload alanı
//            JwtSecurityToken securityToken = new JwtSecurityToken(
//                 issuer: _configuration["Token:Issuer"],
//                 audience: _configuration["Token:Audience"],
//                 expires: token.ExpireDate,
//                 claims: claims,
//                 notBefore: DateTime.Now, // token üretildiği ne kadar zamandan sonra devreye giricek
//                 signingCredentials: signingCredentials
//                );
//            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
//            // Token yaratıldı
//            token.Token = tokenHandler.WriteToken(securityToken);

//            // token ile beraber refresh tokenda yaratıldı. kullanıcının token süresi dolduğunda giriş yapmasına gerek kalmadan yeni bir token üretmek için
//            // token.RefreshToken = CreateRefreshToken();

//            // Şuanda token üzerinde access token var, refresh token var expire date var

//            return new BaseResponse()
//            {
//                Message = token.Token,
//                Status = true,
//            };
//        }

//    }
//}
