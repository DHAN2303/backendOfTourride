using AllrideApi;
using AllrideApi.Hubs;
using AllrideApi.Middleware;
using AllrideApiChat.DataBase;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiRepository.Repositories.Abstract.Buys;
using AllrideApiRepository.Repositories.Abstract.Clubs;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiRepository.Repositories.Abstract.RoutePlannerRepo;
using AllrideApiRepository.Repositories.Abstract.Search;
using AllrideApiRepository.Repositories.Abstract.SocailMedia;
using AllrideApiRepository.Repositories.Concrete;
using AllrideApiRepository.Repositories.Concrete.Buys;
using AllrideApiRepository.Repositories.Concrete.GroupsClubs;
using AllrideApiRepository.Repositories.Concrete.RoutePlannerRepoConcrete;
using AllrideApiRepository.Repositories.Concrete.SocialMedia;
using AllrideApiService.Authentication;
using AllrideApiService.Configuration;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.ClubsInfo;
using AllrideApiService.Services.Abstract.GraphHoperRoundTripRouteApi;
using AllrideApiService.Services.Abstract.GroupsInfo;
using AllrideApiService.Services.Abstract.HereApi;
using AllrideApiService.Services.Abstract.Log;
using AllrideApiService.Services.Abstract.Mail;
using AllrideApiService.Services.Abstract.News;
using AllrideApiService.Services.Abstract.Routes;
using AllrideApiService.Services.Abstract.SocialMedia;
using AllrideApiService.Services.Abstract.TomtomApi;
using AllrideApiService.Services.Abstract.UserMessage;
using AllrideApiService.Services.Abstract.Users;
using AllrideApiService.Services.Abstract.Weather;
using AllrideApiService.Services.Concrete;
using AllrideApiService.Services.Concrete.Clubs;
using AllrideApiService.Services.Concrete.GraphHoperRoundTripRouteApi;
using AllrideApiService.Services.Concrete.Groups;
using AllrideApiService.Services.Concrete.HereApi;
using AllrideApiService.Services.Concrete.Log;
using AllrideApiService.Services.Concrete.Mail;
using AllrideApiService.Services.Concrete.Newss;
using AllrideApiService.Services.Concrete.Notification;
using AllrideApiService.Services.Concrete.Routes;
using AllrideApiService.Services.Concrete.SocialMedia;
using AllrideApiService.Services.Concrete.TomtomApi;
using AllrideApiService.Services.Concrete.UserCommon;
using AllrideApiService.Services.Concrete.UserMessage;
using AllrideApiService.Services.Concrete.Weathers;
using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Serilog;
using Serilog.Core;
using System.Net;
using System.Reflection;
using System.Text;


// string[] args--> komut sat�r� arg�manlar�n� i�eriye almay� sa�l�yor. Yani input lar� i�eriye almam�z� sa�l�yor.




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // Model durumu hatalarını devre dışı bırakır
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Model verileri doğrulama hatası",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = "Gönderilen verilerin doğrulanması sırasında hata oluştu.",
                Instance = context.HttpContext.Request.Path
            };

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });


//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.NumberHandling =
//            System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
//    });


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEndpointsApiExplorer();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Allride API",
        Description = "Allride Endpoint Dokumantasyonu"
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Allride API V2",
        Description = "Allride Endpoint Dokumantasyonu Versiyon 2"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
    });
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    { });
});

var configuration = new ConfigurationBuilder()
      .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
      .AddJsonFile("appsettings.json")
      .Build();

//Add services to the container.
builder.Services.Configure<AccessToken>(builder.Configuration.GetSection("AccessToken"));

builder.Services.AddAndConfigureApiVersioning();

//Logger log = new LoggerConfiguration()
//        .ReadFrom.Configuration(configuration)
//        .WriteTo.PostgreSQL(configuration.GetConnectionString("SqlConnection"), "logs",
//        needAutoCreateTable: true,
//        columnOptions: new Dictionary<string, ColumnWriterBase>
//        {
//            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
//            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
//            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
//            {"times_tamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
//            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
//            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)}
//        })
//        .CreateLogger();

//builder.Logging.ClearProviders();

Logger log = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
builder.Host.UseSerilog(log);

// Http request loglama
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua"); // Kullan c ya dair b t n detaylar  getirir
    logging.ResponseHeaders.Add("TourideLog");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096; // requestin ta  nacak veri limiti art r p azalt labilir
    logging.ResponseBodyLogLimit = 4096;

});


builder.Services.AddSignalR();

// Rate Limiting
builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
//builder.Services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting(); // ????????

var tokenSettingsSection = builder.Configuration.GetSection("TokenOption");
builder.Services.Configure<TokenOption>(tokenSettingsSection);
var tokenSettings = tokenSettingsSection.Get<TokenOption>();
var key = Encoding.UTF8.GetBytes(tokenSettings.SecurityKey);  //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey)),

builder.Services.AddAuthentication(scheme =>
{
scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
options.RequireHttpsMetadata = false;
options.SaveToken = true;
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    LifetimeValidator = (notBefore,expires,securityToken,validationParameters)=> expires != null ? expires > DateTime.UtcNow : false
};

options.Events = new JwtBearerEvents
{
    OnTokenValidated = ctx =>
    {
        return Task.CompletedTask;
    },
    OnAuthenticationFailed = ctx =>
    {
        Console.WriteLine($"Exception : {ctx.Exception.Message}");
        return Task.CompletedTask;
    },
    OnChallenge = ctx =>
    {
        return Task.CompletedTask;
    },
    OnMessageReceived = ctx =>
    {
        return Task.CompletedTask;
    }
};
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory());

builder.Services.AddHttpClient<ITomtomRoutingService, TomtomRoutingService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["TomtomApiBaseUrl:RoutingApiBaseUrl"]);
});
builder.Services.AddHttpClient<IHereRoutingService, HereRoutingService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Here:CalculateRoutes"]);
});
builder.Services.AddHttpClient<IWeatherService, WeatherService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["OpenWeather:WeatherBaseUrl"]);
});


//builder.Services.AddScoped<HttpLoggingMiddleware>();
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(ILoginService), typeof(LoginService));
builder.Services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));
builder.Services.AddScoped(typeof(ITomtomRoutingService), typeof(TomtomRoutingService));
builder.Services.AddScoped(typeof(IHereRoutingService), typeof(HereRoutingService));
builder.Services.AddScoped(typeof(IRoutingRepository), typeof(RoutingRepository));
builder.Services.AddScoped(typeof(IAlongRouteSearchService), typeof(AlongRouteSearchService));
builder.Services.AddScoped(typeof(INewsService), typeof(NewsService));
builder.Services.AddScoped(typeof(INewsRepository), typeof(NewsRepository));
builder.Services.AddScoped(typeof(IUserNewsReactionRepository), typeof(UserNewsReactionRepository));
builder.Services.AddScoped(typeof(IUserGeneralService), typeof(UserGeneralService));
//builder.Services.AddScoped(typeof(IUserGeneralRepository), typeof(UserGeneralRepository));
builder.Services.AddScoped(typeof(IHereRoutingRepository), typeof(HereRoutingRepository));
builder.Services.AddScoped(typeof(IRouteTransportModeRepository), typeof(RouteTransportModeRepository));
builder.Services.AddScoped(typeof(IRouteInstructionDetailRepository), typeof(RouteInstructionDetailRepository));
builder.Services.AddScoped(typeof(IWeatherService), typeof(WeatherService));
builder.Services.AddScoped(typeof(IWeatherRepository), typeof(WeatherRepository));
builder.Services.AddScoped(typeof(ITomTomNearBySearchService), typeof(NearBySearchService));
builder.Services.AddScoped(typeof(IHereNearBySearchService), typeof(HereNearBySearchService));
builder.Services.AddScoped(typeof(ISocialMediaLikeService), typeof(SocialMediaLikeService));
builder.Services.AddScoped(typeof(ISocialMediaPostsService), typeof(SocialMediaPostService));
builder.Services.AddScoped(typeof(ISocialMediaCommentService), typeof(SocialMediaCommentService));
builder.Services.AddScoped(typeof(ISocialMediaFollowService), typeof(SocialMediaFollowService));
builder.Services.AddScoped(typeof(ISocialMediaStoryService), typeof(SocialMediaStoryService));
builder.Services.AddScoped(typeof(IUsageTrackerService), typeof(UsageTrackerService));
builder.Services.AddScoped(typeof(IUserDeleteService), typeof(UserDeleteService));
builder.Services.AddScoped(typeof(IUserUpdateService), typeof(UserUpdateService));
builder.Services.AddScoped(typeof(IRoutesServices), typeof(RouteServices));
builder.Services.AddScoped(typeof(IRouteUserFetchRepository), typeof(RouteUserFetchRepository));
//builder.Services.AddScoped(typeof(IUserMessagesRepository), typeof(UserMessagesRepository));
builder.Services.AddScoped(typeof(IUserMessageService), typeof(UserMessageService));
builder.Services.AddScoped(typeof(ISocialMediaRepository), typeof(SocialMediaRepository));
builder.Services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
builder.Services.AddScoped(typeof(IGroupService),typeof(GroupService));
builder.Services.AddScoped(typeof(IClubService), typeof(ClubService));
builder.Services.AddScoped(typeof(IClubRepository), typeof(ClubRepository));
builder.Services.AddScoped(typeof(IBuyRepository), typeof(BuyRepository));
builder.Services.AddScoped(typeof(ISearchService), typeof(SearchService));
builder.Services.AddScoped(typeof(ISearchRepository), typeof(SearchRepository));
builder.Services.AddScoped(typeof(IClubSettingsRepository), typeof(ClubSettingsRepository));
builder.Services.AddScoped(typeof(IClubSettingsService), typeof(ClubSettingsService));
builder.Services.AddScoped(typeof(IGroupSettingsService), typeof(GroupSettingsService));
builder.Services.AddScoped(typeof(IGroupSettingsRepository), typeof(GroupSettingsRepository));
builder.Services.AddScoped(typeof(IMailService), typeof(MailService));
builder.Services.AddScoped(typeof(IRoutePlannerService), typeof(RoutePlannerService));
builder.Services.AddScoped(typeof(IRoutePlannerRepository), typeof(RoutePlannerRepository));
builder.Services.AddScoped(typeof(IRoundTripRouteService), typeof(RoundTripRouteService));


// for notification
builder.Services.AddScoped(typeof(IPushNotificationService), typeof(PushNotificationService));
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<NotificationCatch>();

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<AllrideApiDbContext>(x => x.UseNpgsql(connectionString,
    x => x.UseNetTopologySuite()
));
builder.Services.AddScoped<IPosts, Posts>();
builder.Services.AddScoped(typeof(ILogApiService), typeof(LogApiService));
//var automapper = new MapperConfiguration(item => item.AddProfile(new MapperProfile()));
//IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(provider =>
new MapperConfiguration(config =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    config.AddProfile(new MapperProfile(geometryFactory));
}).CreateMapper()
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options => {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
if (!app.Environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider("/var/www/countries"
               ),
        RequestPath = "/static-country-map"
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider("/var/www/videos"
              ),
        RequestPath = "/videos"
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider("/var/www/images"
              ),
        RequestPath = "/images"
    });
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        // Hata detaylarını almak için exception objesini kullanabilirsiniz

        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Sunucu hatası",
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = "Sunucuda bir hata oluştu.",
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});




//app.UseMiddleware<HttpLoggingMiddleware>();
//app.UseIpRateLimiting();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseRouting();

// Add the JwtMiddleware before the authentication middleware
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();

// Add the authorization middleware after the authentication middleware
app.UseAuthorization();

// Add any other middleware after the authorization middleware
app.UseMiddleware<HttpLoggingApiMiddleware>();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHubs>("/chatHubs");
});
app.Run();

