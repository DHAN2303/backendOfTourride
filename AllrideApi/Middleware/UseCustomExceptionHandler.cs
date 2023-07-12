using AllrideApiCore.Dtos;
using AllrideApiService.Exceptions;
using AllrideApiService.Response;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace AllrideApi.Middleware
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    // response un content tipi
                    context.Response.ContentType = "application/json";
                    // Bu interface üzerinden uygulamada fırlatılan hatayı alıyoruz
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponse<NoContentDto>.Fail(statusCode,exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    
                });
            });
        }
    }
}
