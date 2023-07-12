
using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Dtos.Update;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Authentication;
using AllrideApiService.Configuration.Extensions;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Mail;
using AllrideApiService.Services.Abstract.Users;
using AutoMapper;
using DTO.Select;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllrideApiService.Services.Concrete.UserCommon
{
    public class LoginService : ILoginService
    {
        private const string V = "Login Servis Katmanındaki Error Log: ";
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginService> _logger;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public LoginService(ILoginRepository loginRepository,
            IConfiguration configuration, IMapper mapper, ILogger<LoginService> logger, IMailService mailService)
        {
            _logger = logger;
            _loginRepository = loginRepository;
            _configuration = configuration;
            _mapper = mapper;
            _mailService = mailService;
        }

        public CustomResponse<LoginUserResponseDto> Login(LoginUserDto userDto)
        {
            LoginUserResponseDto responseDto = new();
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            string token = null;
            try
            {
                var validator = new LoginUserValidation();
                var isValid = validator.Validate(userDto).ThrowIfException();

                if (isValid.Status == false) //isValid.ListErrorEnums.Count>0
                {
                    // customResponse.Errors = isValid.ErrorCode;
                    return CustomResponse<LoginUserResponseDto>.Fail(isValid.ErrorEnums);
                }

                var verifyPass = VerifyPassword(userDto); // Şifre Doğrulama

                if (verifyPass.Status == false)
                {
                    foreach (ErrorEnumResponse response in verifyPass.ErrorEnums)
                    {
                        _enumListErrorResponse.Add(response);
                    }
                    return CustomResponse<LoginUserResponseDto>.Fail(_enumListErrorResponse, false);
                }
                else
                {
                    token = generateJwtToken(verifyPass.Data);
                    var userDetailDatas = _loginRepository.GetUserDetail(verifyPass.Data);
                    responseDto = _mapper.Map<LoginUserResponseDto>(userDetailDatas);
                    responseDto.Token = token;
                    responseDto.Email = verifyPass.Data.Email;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(V + ex.Message);

            }

            if (!token.IsNullOrEmpty())
            {
                _logger.LogInformation("----------------Token Oluşturuldu-------------------:  " + token.ToString());

            }
            else
            {
                _enumListErrorResponse.Add(ErrorEnumResponse.FailedToCreateToken);
                _logger.LogError(" ----------------Token Oluşmadı---------------- ");
                return CustomResponse<LoginUserResponseDto>.Fail(_enumListErrorResponse, false);
            }
            return CustomResponse<LoginUserResponseDto>.Success(responseDto, true);

        }
        private string generateJwtToken(UserEntity user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.UTF8.GetBytes(_accessToken.SecurityKey);
            string secretSection = _configuration.GetSection("TokenOption").GetSection("SecurityKey").Value;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(
                    "id", user.Id.ToString(),
                    "expireDate", "30"),
                    new Claim ("email",user.Email),
                }),
                // Expires = DateTime.UtcNow.AddDays(7),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSection)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public CustomResponse<UserEntity> VerifyPassword(LoginUserDto userDto)
        {
            UserEntity user = _mapper.Map<UserEntity>(userDto);
            var registeredUser = GetUser(user);
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            try
            {
                if (registeredUser == null || registeredUser.Email == null)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.EmailIsNotRegistered);
                    return CustomResponse<UserEntity>.Fail(_enumListErrorResponse, false);
                }
                var result = HashHelper.VerifyPasswordHash(userDto.Password, registeredUser.UserPassword.HashPass, registeredUser.UserPassword.SaltPass);
                if (result == false)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.PasswordInvalid);
                    return CustomResponse<UserEntity>.Fail(_enumListErrorResponse, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(V + " " + ex.Message);
            }
            return CustomResponse<UserEntity>.Success(registeredUser, true);
        }
        public UserEntity GetUser(UserEntity user)
        {
            user = _loginRepository.GetUserWithPassword(user);
            return user;
        }
        public UserEntity GetUserById(int Id)
        {
            return _loginRepository.GetUserById(Id);
        }

        // ŞİFREMİ UNUTTUM MAİLİ GÖNDERME
        public async Task<CustomResponse<UserUpdateDto>> ForgotPassword(ForgotPasswordDto userUpdateDto)
        {
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            try
            {
                // Validasyonlar Yapıldı
                var validator = new UpdateUserValidation();
                var isValid = validator.Validate(userUpdateDto).ThrowIfException();

                CustomResponse<UserUpdateDto> customResponse = new();

                // validasyon ile ilgili bir hata varsa response olarak dönüldü
                if (isValid.Status == false)
                {
                    return CustomResponse<UserUpdateDto>.Fail(isValid.Errors);
                }

                // Veritabanında bu mail adresi var mı kontrol edildi
                UserEntity user = _mapper.Map<UserEntity>(userUpdateDto);
                user = _loginRepository.ForgotPassword(user);
                if (user == null || string.IsNullOrEmpty(user.Email))
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.EmailIsNotRegistered);
                    return CustomResponse<UserUpdateDto>.Fail(_enumListErrorResponse, false);
                }

                // Eğer veritabanında mail adresi varsa. Kullanıcının mail adresine mail göndericek
                var actCode = RandomActivationCode();
                await _mailService.SendMessageAsync(user.Email, $"{actCode}", "<b> Bu bir test mailidir <b>");
                return CustomResponse<UserUpdateDto>.Success(true);

            }
            catch (Exception ex)
            {
                _logger.LogError(V + ex.Message);
                return CustomResponse<UserUpdateDto>.Fail(_enumListErrorResponse, false);
            }

        }

        // ŞİFREMİ UNUTTUM MAİLİ İÇİN GÖNDERİLEN RESET CODE U DOĞRULAMA
        // Önce Kodu Doğrulayacak Daha sonra User Password Reset Servisine istek göndericek
        public CustomResponse<object> VerifyResetCode(ForgotPasswordDto forgotPasswordDto)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                // Veritabanındaki reset code kullanıcıdan gelen reset code ile aynı değil ise  hata dönüyor
                // Bu da şu demek kullanıcı maile gelen kodu yanlış girmiş.
                // Eğer Code doğruysa Code u sıfırlamasın burası o code u dönsün

                if (forgotPasswordDto.ResetCode <= 1000 && forgotPasswordDto.ResetCode >= 10000)
                {
                    errors.Add(ErrorEnumResponse.FailedToActivationCode);
                    return CustomResponse<object>.Fail(errors, false);
                }

                var validator = new UpdateUserValidation();
                var isValid = validator.Validate(forgotPasswordDto).ThrowIfException();

                if (isValid.Status == false)
                {
                    return CustomResponse<object>.Fail(isValid.Errors);
                }
                // Veritabanında bu mail adresine ait reset code var mı kontrol edildi

                UserEntity user = _mapper.Map<UserEntity>(forgotPasswordDto);

                var resultUser = _loginRepository.GetUser(user);
                if (resultUser.forgot_password_code != forgotPasswordDto.ResetCode)
                {
                    errors.Add(ErrorEnumResponse.FailedToActivationCode);
                    return CustomResponse<object>.Fail(errors, false);
                }
                else
                {
                    return CustomResponse<object>.Success(resultUser.forgot_password_code, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" LOGIN SERVICE LOG ERROR VERIFY ACTVIVATION CODE " + ex.Message);
                return CustomResponse<object>.Fail(errors, false);
            }
        }

        // Kullanıcının Şifresini Güncelleme
        public CustomResponse<NoContentDto> UserPasswordReset(CreateUserResetPasswordDto createUserResetPasswordDto)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                var validator = new CreateUserPasswordValidation();
                var response = validator.Validate(createUserResetPasswordDto).ThrowIfException();
                if (response.Status == false)
                {
                    return response;
                }
                UserEntity user = _mapper.Map<UserEntity>(createUserResetPasswordDto);
                var isExistUserEmail = _loginRepository.GetUserMail(user.Email);
                if (isExistUserEmail == null)
                {
                    errors.Add(ErrorEnumResponse.EmailIsNotRegistered);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }
                if (createUserResetPasswordDto.Password == createUserResetPasswordDto.PasswordConfirm)
                {
                    byte[] passwordHash, passwordSalt;  // Şifreleme bilgilerini tutacak için hash ve salt değişkenleri

                    HashHelper.CreatePasswordHash(createUserResetPasswordDto.Password, out passwordHash, out passwordSalt);
                    user.UserPassword.SaltPass = passwordSalt;
                    user.UserPassword.HashPass = passwordHash;
                    _loginRepository.SaveChanges();
                }

                errors.Add(ErrorEnumResponse.PasswordConfirmMismatch);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(" LOGIN SERVICE LOG ERROR UserPasswordReset METHOD:  " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }
        }           
      
        public Task<CustomResponse<NoContentDto>> SendForgotPasswordMail(string UserMail)
        {
            throw new NotImplementedException();
        }

        public static int StringToInt(string code)
        {
            bool Convert = int.TryParse(code, out int actCode);
            if (Convert)
                return actCode;
            return 0;
        }
        public static string RandomActivationCode()
        {
            var random = new Random();
            int activationCode = random.Next(1000, 10000);
            return activationCode.ToString();

        }
        public int? TokenValidate(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["SecurityKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch (Exception ex)
            {
                _logger.LogError(V + ex.Message);
                // return null if validation fails
                return null;
            }
        }
    }
}


//public CustomResponse<LoginUserDto> VerifyPassword(LoginUserDto userDto)
//{
//    var user = GetUser(userDto);
//    try
//    {
//        if (user == null || user.Email == null)
//        {
//            return CustomResponse<LoginUserDto>.Fail(false, EnumResponse.NotFoundEmail);
//        }
//        var result = HashHelper.VerifyPasswordHash(userDto.Password, user.UserPassword.HashPass, user.UserPassword.SaltPass);
//        if (result == false)
//        {
//            ////throw new ClientSideException($"{userDto.Email} not found");
//            //_logger.LogError($"Failed to verify {userDto.Email}");
//            return CustomResponse<LoginUserDto>.Fail(false, EnumResponse.PasswordFailed);
//        }
//    }
//    catch(Exception ex)
//    {
//        _logger.LogError(V + " " + ex.Message);
//    }

//    return CustomResponse<LoginUserDto>.Success(true);
//}

// CREATE TOKEN

// var u = user.ToString();
// var e = AsymmetricEncryption.EncryptText(u);
//string secretSection = _configuration.GetSection("TokenOption").GetSection("SecurityKey").Value;
//var claims = new Claim[]
//{
//    new Claim(ClaimTypes.Email,user.Email),
//};
//token = GenerateToken.Generate(new AccessToken
//{
//    Claims = claims,
//    ExpireDate = DateTime.UtcNow.AddMinutes(5),
//    SecurityKey = secretSection,

//});



//public EnumResponse VerifyPassword(LoginUserDto userDto)
//{
//    User user = _mapper.Map<User>(userDto);
//    var registeredUser = GetUser(user);
//    try
//    {
//        if (registeredUser == null || registeredUser.Email == null)
//        {
//            return EnumResponse.UserLoginFailed;
//        }
//        var result = HashHelper.VerifyPasswordHash(userDto.Password, registeredUser.UserPassword.HashPass, registeredUser.UserPassword.SaltPass);
//        if (result == false)
//        {
//            ////throw new ClientSideException($"{userDto.Email} not found");
//            //_logger.LogError($"Failed to verify {userDto.Email}");
//            return  EnumResponse.UserLoginFailed;
//        }
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(V + " " + ex.Message);
//    }

//    return EnumResponse.UserIsFound;
//}


/*
 

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

 */

/*  TOKEN CASE 1:
 *                  AccessToken token = new AccessToken();

                 tokenı oluşturabilmek için gerekli key oluşturuldu
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
                 oluşan keyi şifreliyor. Aslında şifrelenmiş bir kimlik oluşturuyor.
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                token.ExpireDate = DateTime.Now.AddMinutes(1);

                 Claim bazlı kimlik doğrulama, Payload alanı
                
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.Name),
                };

                yukarıdaki bilgileri ve configdeki kullanarak token oluşturyoruz.
                Tokenın tipi JwtSecurity. Token ayarlarını oluşturduk.
                Token oluştuma şeklini belirledik tokenımız hazır.
                Payload alanı
                JwtSecurityToken securityToken = new JwtSecurityToken(
                     issuer: _configuration["Token:Issuer"],
                     audience: _configuration["Token:Audience"],
                     expires: token.ExpireDate,
                     claims: claims,
                     notBefore: DateTime.Now, // token üretildiği ne kadar zamandan sonra devreye giricek
                     signingCredentials: signingCredentials
                    );
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                 Token yaratıldı
                token.Token = tokenHandler.WriteToken(securityToken);

                 token ile beraber refresh tokenda yaratıldı. kullanıcının token süresi dolduğunda giriş yapmasına gerek kalmadan yeni bir token üretmek için
                 token.RefreshToken = CreateRefreshToken();

                 Şuanda token üzerinde access token var, refresh token var expire date var
*/

/* TOKEN CASE 2:
 *  if (verifyPassword.Status)
            {
                var user = GetUser(userDto);
                string secretSection = _configuration.GetSection("TokenOption").GetSection("SecurityKey").Value;
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                };
                string token = GenerateToken.Generate(new AccessToken
                {
                    Claims = claims,
                    ExpireDate = DateTime.UtcNow.AddMinutes(5),
                    SecurityKey = secretSection
                });;

                response.Message = token;
                response.Status = true;
                return response;
            }
 * 
 * 
 * 
 * 
 */