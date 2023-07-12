//using AllrideApiCore.Entities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using RTools_NTS.Util;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace AllrideApiService.Authentication
//{
//    public class GenerateToken
//    {
//        private readonly IConfiguration _configuration;

//        public GenerateToken(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        public static string Generate(AccessToken accessToken)
//        {
//            AccessToken token = new();
//            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

//            //Şifrelenmiş kimliği oluşturuyoruz.
//            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

//            //Oluşturulacak token ayarlarını veriyoruz.
//            token.Expiration = DateTime.UtcNow.AddSeconds(second);
//            JwtSecurityToken securityToken = new(
//                audience: _configuration["Token:Audience"],
//                issuer: _configuration["Token:Issuer"],
//                expires: token.Expiration,
//                notBefore: DateTime.UtcNow,
//                signingCredentials: signingCredentials,
//                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
//                );

//            //Token oluşturucu sınıfından bir örnek alalım.
//            JwtSecurityTokenHandler tokenHandler = new();
//            token.Token = tokenHandler.WriteToken(securityToken);

//            //string refreshToken = CreateRefreshToken();

//            token.RefreshToken = CreateRefreshToken();
//            return token;
//        }

//    }
//}



//using AllrideApiCore.Entities;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace AllrideApiService.Authentication
//{
//    public class GenerateToken
//    {
//        public static string Generate(AccessToken accessToken)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(accessToken.Claims),
//                Expires = accessToken.ExpireDate,
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessToken.SecurityKey)), SecurityAlgorithms.HmacSha256)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
