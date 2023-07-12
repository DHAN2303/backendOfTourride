using Microsoft.AspNetCore.Mvc.Versioning;

namespace AllrideApi
{
    public static class TourideApiVersioning
    {
        public static IServiceCollection AddAndConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.ErrorResponses = new DefaultErrorResponseProvider();
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                   new HeaderApiVersionReader("touride-api-version"),
                   new MediaTypeApiVersionReader("touride-media-type-api-vers"));


                //new UrlSegmentApiVersionReader(),
                //new QueryStringApiVersionReader("api-version"),
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";  // Dışarıdan gelen versioning in alacağı formatı söylüyor                   
                    options.SubstituteApiVersionInUrl = true; // Bu versiyonu url de kullanılacağında true
                });

            return services;
        }
    }
}


//builder.Services.AddApiVersioning(_ =>
//{
//    _.DefaultApiVersion = new ApiVersion(1, 0);
//    _.AssumeDefaultVersionWhenUnspecified = true;  // Default Api Version kullanmak için set edildi.
//    _.ApiVersionReader = new HeaderApiVersionReader("touride-api-version1");
//    //_.ReportApiVersions = true; // Birden fazla controller'a sahip olan bir controllerın desteklediği versiyonları clienta bildirmek için kullanılan özellik
//});


