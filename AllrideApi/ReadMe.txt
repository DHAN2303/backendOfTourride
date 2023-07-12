
TOKEN İÇİN İLK CASE AUTH SERVİCE DE YAZILDI
//            { Case 1:

//                // Burada Jwt token işlemlerini yapacağım
//                AccessToken token= new AccessToken();

//                // tokenı oluşturabilmek için gerekli key oluşturuldu
//                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
//;               // oluşan keyi şifreliyor. Aslında şifrelenmiş bir kimlik oluşturuyor.
//                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                token.ExpireDate = DateTime.Now.AddMinutes(1);
//                //yukarıdaki bilgileri ve configdeki kullanarak token oluşturyoruz.
//                //Tokenın tipi JwtSecurity. Token ayarlarını oluşturduk. Token oluştuma şeklini belirledik tokenımız hazır.
//                JwtSecurityToken securityToken = new JwtSecurityToken(
//                     issuer: _configuration["Token:Issuer"],
//                     audience: _configuration["Token:Audience"],
//                     expires:token.ExpireDate,
//                     notBefore:DateTime.Now,
//                     signingCredentials: signingCredentials
//                    );
//                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
//                // Token yaratıldı
//                token.AccessToken = tokenHandler.WriteToken(securityToken);

//                // token ile beraber refresh tokenda yaratıldı. kullanıcının token süresi dolduğunda giriş yapmasına gerek kalmadan yeni bir token üretmek için
//                token.RefreshToken = CreateRefreshToken();

//                return token;

//                // Şuanda token üzerinde access token var, refresh token var expire date var


// refresh token için string oluşturuldu
//public string CreateRefreshToken()
//{
//    return Guid.NewGuid().ToString();
//}


TOKEN PROGRAM.CS CASE 1
// Add services to the container.
//Case 1: 
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer()

// Token Case:1
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters // tokenın nasıl valide edileceğinin parametrelerini alıyor.
//    {

//        ValidateIssuer = true, //  tokenın sağlayıcısı/dağıtıcısı kim
//        ValidateAudience = true, // tokenı kimler kullanabilir
//        ValidateLifetime = false, // Life time ı kontrol et. life time tamamlandıysa token expire olsun ve yetkilendirmeyi kapat erişelemesi
//        ValidateIssuerSigningKey = true, // tokenı kriptolıycağımız anahtar key
//        ValidIssuer = builder.Configuration["Token:Issuer"], // tokenın yaratılırkenki ıssure bunlardır
//        ValidAudience = builder.Configuration["Token:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//        (Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
//        ClockSkew = TimeSpan.Zero // tokenı üre sunucudaki time zone ile tokenı kullanacak olan
//                                  // clientlerın time zone u birbirinde farklı olduğunda tokenın adil bir şekilde dağıtılabilmesini sağlıyor
//                                  // Tokenın expration date ini ekleme yapıyor. Şuanda zero olduğu için bu optimizasyonu sağlamadım hiçbir süre eklemiyor veya çıkarmıyor.
//    };
//});
CASE 2:
//var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOption>();
// builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true, //  tokenın sağlayıcısı/dağıtıcısı kim
//            ValidateAudience = true, // tokenı kimler kullanabilir
//            ValidateLifetime = true, // Life time ı kontrol et. life time tamamlandıysa token expire olsun ve yetkilendirmeyi kapat erişelemesi
//            ValidIssuer = tokenOptions.Issuer,  // tokenın yaratılırkenki ıssure bunlardır
//            ValidAudience = tokenOptions.Audience,
//            ValidateIssuerSigningKey = true, // tokenı kriptolıycağımız anahtar key
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
//            ClockSkew = TimeSpan.Zero // tokenı üre sunucudaki time zone ile tokenı kullanacak olan
//                                      // clientlerın time zone u birbirinde farklı olduğunda tokenın adil bir şekilde dağıtılabilmesini sağlıyor
//                                      // Tokenın expration date ini ekleme yapıyor. Şuanda zero olduğu için bu optimizasyonu sağlamadım hiçbir süre eklemiyor veya çıkarmıyor.
//        };
//    });


//builder.Services.AddDbContext<AllrideApiDbContext>(x =>
//{
//    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), option =>
//    {
//        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AllrideApiDbContext)).GetName().Name);
//    });
//});



ADD AUTOMAPPER


//builder.Services.AddAutoMapper(typeof(UserService));
//builder.Services.AddAutoMapper(config => config.AddProfile(new MapperProfile()));

        // CASE 2
        //var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOption>();

        //var expireDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
        ////Token içerisinde tuttuğumuz bilgiler
        //var claims = new Claim[]
        //{
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim(ClaimTypes.GivenName, user.Name),
        //    new Claim(ClaimTypes.DateOfBirth,user.BirthDate.ToString()),
        //    //new Claim("UserPermissionAddCache",StringHelper.CreateCacheKey(user.Name,user.Id))
        //};

        //SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
        //var jwtToken = new JwtSecurityToken(
        //    issuer: tokenOptions.Issuer,
        //    audience: tokenOptions.Audience,
        //    claims: claims,
        //    expires: expireDate,
        //    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        //);

        //var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        ////kullanıcının id ile USR_kullanıcıID key oluşup token kaydedilecek

        //return new AccessToken()
        //{
        //    Token = token,
        //    ExpireDate = expireDate
        //};
