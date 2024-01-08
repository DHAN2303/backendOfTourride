//namespace WebApi.Helpers;

//using AllrideApiService.Abstract;
//using AllrideApiService.Authentication;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using static Org.BouncyCastle.Math.EC.ECCurve;

//public class JwtMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly AccessToken _aceessToken;
//    private readonly IConfiguration _configuration;

//    public JwtMiddleware(RequestDelegate next, IOptions<AccessToken> aceessToken, IConfiguration config)
//    {
//        _next = next;
//        _aceessToken = aceessToken.Value;
//        _configuration = config;
//    }

//    public async Task Invoke(HttpContext context, ILoginService userService)
//    {
//        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//        if (token != null)
//            AttachUserToContext(context, userService, token);

//        await _next(context);
//    }

//    private void AttachUserToContext(HttpContext context, ILoginService userService, string token)
//    {
//        try
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            //var key = Encoding.ASCII.GetBytes(_aceessToken.SecurityKey);
//            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenOption").GetSection("SecurityKey").Value);

//            tokenHandler.ValidateToken(token, new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(key),
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
//                ClockSkew = TimeSpan.Zero
//            }, out SecurityToken validatedToken);

//            var jwtToken = (JwtSecurityToken)validatedToken;
//            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

//            // attach user to context on successful jwt validation
//            context.Items["User"] = userService.GetUserById(userId);
//        }
//        catch(Exception ex) 
//        {
//            // do nothing if jwt validation fails
//            // user is not attached to context so request won't have access to secure routes
//            Console.WriteLine(ex);
//        }
//    }
//}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenOption").GetSection("SecurityKey").Value);

            try
            {
                // Burada token validate ediliyor. Token geçresizse hata fırlatıyor catche geçiyor.
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string userId = null;
                string email = null;
                foreach (var d in jwtToken.Claims)
                {
                    if (d.Type == "id")
                        userId = d.Value;
                    if(d.Type == "email")
                        email = d.Value;
                }
               // var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
               //var email = int.Parse(jwtToken.Claims[1](x => x.Type == "email").Value);

                // set user identity
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()), new Claim(ClaimTypes.Email, email.ToString()) };
                var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                context.User = new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                // token validation failed
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                Console.WriteLine(ex);
                await context.Response.WriteAsync("Invalid token");
                return;
            }
        }

        await _next(context);
    }
}
