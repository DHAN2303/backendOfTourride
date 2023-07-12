using AllrideApiService.Services.Abstract.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllrideApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, ILoginService loginService)
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
                    var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                    var email = jwtToken.Claims.Last(x => x.Type == "email").Value;

                    // attach user to context on successful jwt validation
                    var result = loginService.GetUserById(userId);

                    if (result.Id < 0 || result.ActiveUser == false || result == null || result.DeletedDate < DateTime.UtcNow || result.DeletedDate != null)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Invalid token");
                        return;
                    }

                    //string userId = null;
                    //string email = null;
                    //foreach (var d in jwtToken.Claims)
                    //{
                    //    if (d.Type == "id")
                    //        userId = d.Value;
                    //    if (d.Type == "email")
                    //        email = d.Value;
                    //}

                    // set user identity

                    context.Items["User"] = result;
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
}