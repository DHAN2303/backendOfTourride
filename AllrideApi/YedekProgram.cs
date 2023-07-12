//using AllrideApi;
//using AllrideApi.Middleware;
//using AllrideApiRepository;
//using AllrideApiRepository.Repositories.Abstract;
//using AllrideApiRepository.Repositories.Concrete;
//using AllrideApiService.Abstract;
//using AllrideApiService.Abstract.HereApi;
//using AllrideApiService.Abstract.TomtomApi;
//using AllrideApiService.Concrete;
//using AllrideApiService.Concrete.HereApi;
//using AllrideApiService.Configuration;
//using AllrideApiService.Configuration.Validator;
//using AutoMapper;
//using FluentValidation;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using NetTopologySuite;
//using NetTopologySuite.Geometries;
//using NpgsqlTypes;
//using Serilog;
//using Serilog.Core;
//using Serilog.Events;
//using Serilog.Sinks.PostgreSQL;
//using System.Security.Claims;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ConfigureEndpointDefaults(listenOptions =>
//    {});
//});

////Add services to the container.
//var tokenSettingsSection = builder.Configuration.GetSection("TokenOption");
//builder.Services.Configure<TokenOption>(tokenSettingsSection);
//var tokenSettings = tokenSettingsSection.Get<TokenOption>();
//var key = Encoding.UTF8.GetBytes(tokenSettings.SecurityKey);  //new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey)),

//builder.Services.AddAndConfigureApiVersioning();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
//{
//    o.RequireHttpsMetadata = false;
//    o.SaveToken = true;
//    o.TokenValidationParameters = new TokenValidationParameters // token�n nas�l valide edilece�inin parametrelerini al�yor.
//    {
//        ValidateIssuer = true, //  token�n sa�lay�c�s�/da��t�c�s� kim
//        ValidateAudience = true, // token� kimler kullanabilir
//        ValidateLifetime = false, // Life time � kontrol et. life time tamamland�ysa token expire olsun ve yetkilendirmeyi kapat eri�elemesi
//        ValidateIssuerSigningKey = true, // token� kriptol�yca��m�z anahtar key
//        ValidIssuer = tokenSettings.Issuer,//builder.Configuration["Token:Issuer"], // token�n yarat�l�rkenki �ssure bunlard�r
//        ValidAudience = tokenSettings.Audience,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        // token� �re sunucudaki time zone ile token� kullanacak olan
//        // clientler�n time zone u birbirinde farkl� oldu�unda token�n adil bir �ekilde da��t�labilmesini sa�l�yor
//        // Token�n expration date ini ekleme yap�yor. �uanda zero oldu�u i�in bu optimizasyonu sa�lamad�m hi�bir s�re eklemiyor veya ��karm�yor.
//        ClockSkew = TimeSpan.Zero,
  
//        // JWT üzerinden Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.
//        NameClaimType = ClaimTypes.Name
//    }; 
//   // Request neticesinde gelen token�n do�rulanma, iptal edilme, de�i�me yahut kabul edilme
//   // durumlar�ndan haberdar olabilmemiz i�in ili�kili eventlar tan�mlanm��t�r.
//    o.Events = new JwtBearerEvents
//    {
//        /*
//         * E�er ki, taleple birlikte g�nderilen token ge�erliyse buras� tetiklenmekte ve 
//         * do�rulama ile ilgili i�lemler ger�ekle�tirilmektedir.
//         * Ard�ndan requestin as�l hedefi olan controller tetiklenmektedir.
//         */
//        OnTokenValidated = ctx => {
//            //Gerekirse burada gelen token i�erisindeki �e�itli bilgilere g�re do�rulam yap�labilir.
//            return Task.CompletedTask;
//        },
//        //Yok e�er taleple birlikte gelen token ge�ersiz, eskimi� yahut bozuk ise bu event tetiklenecektir.
//        OnAuthenticationFailed = ctx => {
//            Console.WriteLine("Exception:{0}", ctx.Exception.Message);
//            return Task.CompletedTask;
//        },
//        OnChallenge = ctx =>
//        {
//            return Task.CompletedTask;
//        },
//        //Client�tan gelen t�m talepleri, token olsun olmas�n ilk kar��layan ve kabul eden eventt�r.
//        OnMessageReceived = ctx =>
//        {
//            return Task.CompletedTask;
//        }
//    };
//});

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
//});

//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();

//// Loglama Konfig�rasyonlar� burada yaz�ld�
//Logger log = new LoggerConfiguration()
//    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)  //  Konsola loglama yapma
//                                                                           //.WriteTo.File("log.txt",
//                                                                           //   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
//    .WriteTo.File("log.txt",
//                rollingInterval: RollingInterval.Day,
//                rollOnFileSizeLimit: true)// Txt dosyas�na loglama yapma
//    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("SqlConnection"), "logs",
//        // uygulama ayağa�a kalkt���nda vermi� oldu�umuz connection stringde tablo yoksa olu�turacak ekstra column olu�turmak i�in
//        needAutoCreateTable: true,
//        columnOptions: new Dictionary<string, ColumnWriterBase>
//        {
//            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
//            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
//            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
//            {"times_tamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
//            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
//            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
//            //{"user", new UserColumnWriter()}
//        })
//    .Enrich.FromLogContext() // Harici propertyleri yani user �n ad� gibi login olan register olan kullan�c� gibi �zelle�tirdi�imiz propertyleri al�yor.
//    .MinimumLevel.Information() // Information �zerinden loglamalar� yapabiliriz
//    .CreateLogger();

//// Serilog 
//builder.Host.UseSerilog(log);

//builder.Services.AddEndpointsApiExplorer();

//// Http request loglama
////builder.Services.AddHttpLogging(logging =>
////{
////    logging.LoggingFields = HttpLoggingFields.All;
////    logging.RequestHeaders.Add("sec-ch-ua"); // Kullanıcıya dair bütün detayları getirir
////    logging.ResponseHeaders.Add("TourideLog");
////    logging.MediaTypeOptions.AddText("application/javascript");
////    logging.RequestBodyLogLimit = 4096; // requestin taşınacak veri limiti artırıp azaltılabilir
////    logging.ResponseBodyLogLimit = 4096;

////});

//builder.Services.AddSwaggerGen(options =>
//    {
//        options.SwaggerDoc("v1", new OpenApiInfo
//        {
//            Version = "v1",
//            Title = "Allride API",
//            Description = "Allride Endpoint Dok�mantasyonu"
//        });
//        options.SwaggerDoc("v2", new OpenApiInfo
//        {
//            Version = "v2",
//            Title = "Allride API V2",
//            Description = "Allride Endpoint Dok�mantasyonu Versiyon 2"
//        });
//    }
//);
//builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory());
////builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>(c =>
////{
////    c.BaseAddress = new Uri("https://api.tomtom.com/routing/1/calculateRoute/52.50931%2C13.42936%3A52.50274%2C13.43872/json");
////});
//builder.Services.AddHttpClient<ITomtomRoutingService,TomtomRoutingService>(c =>
//{
//    c.BaseAddress = new Uri(builder.Configuration["TomtomApiBaseUrl:RoutingApiBaseUrl"]);
//});
//builder.Services.AddHttpClient<IRouteCalculationService, RouteCalculationService>(c =>
//{
//    c.BaseAddress = new Uri(builder.Configuration["Here:CalculateRoutes"]);
//});
//builder.Services.AddScoped(typeof(IUserRegisterRepository), typeof(UserRegisterRepository));
//builder.Services.AddScoped(typeof(IUserRegisterService), typeof(UserRegisterService));
//builder.Services.AddScoped(typeof(ILoginService), typeof(LoginService));
//builder.Services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));
//builder.Services.AddScoped(typeof(ITomtomRoutingService), typeof(TomtomRoutingService));
//builder.Services.AddScoped(typeof(IRouteCalculationService),typeof(RouteCalculationService));
//builder.Services.AddScoped(typeof(IRoutingRepository), typeof(RoutingRepository));
//builder.Services.AddScoped(typeof(IAlongRouteSearchService), typeof(AlongRouteSearchService));
//var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
//builder.Services.AddDbContext<AllrideApiDbContext>(x => x.UseNpgsql(connectionString, 
//    x => x.UseNetTopologySuite()
//));

////var automapper = new MapperConfiguration(item => item.AddProfile(new MapperProfile()));
////IMapper mapper = automapper.CreateMapper();
//builder.Services.AddSingleton(provider =>
//new MapperConfiguration(config =>
//    {
//        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
//        config.AddProfile(new MapperProfile(geometryFactory));
//    }).CreateMapper()
//);
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//var app = builder.Build();
//var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//    app.UseSwagger(options =>
//    {
//        options.SerializeAsV2 = true;
//    });
//    app.UseSwaggerUI(options=>{
//        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
//        {
//            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
//                description.GroupName.ToUpperInvariant());
//        }
//    });
//}

//app.UseHttpsRedirection();
//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseMiddleware<HttpLoggingMiddleware>();
//app.UseHttpLogging();
//app.UseSerilogRequestLogging(); // Yaz�ld��� yerden sonraki i�lemleri loglayacak yani sadce a�a��daki durumlar�n loglanmas� yap�lacak
//app.UseAuthentication(); // UseAuthorization dan önce authentication kullanmak gerekiyor. �nce authorization kullan�rsak kullan�c� giri� yapamaz.
//app.UseAuthorization();

//// Burası Enrich kullanımına bir örnektir.
////app.Use(async (context, next) =>
////{
////    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
////    LogContext.PushProperty("user_name", username);
////    await next();
////});
////app.UseMiddleware<HttpLoggingMiddleware>();

//app.MapControllers();

//app.Run();



//// ActionFilterAttribute s�n�f�n� kullanarak ortak bir response yap�s� olu�turdum. Her controller�n ba��nda
//// �a��rmamak i�in Program.cs e ekledim
////builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttributeController()));
//// Api tarf�nda validation default olarak aktif. Yani fluent validation�n d�nm�� oldu�u model aktif ediliyor.
//// O y�zden bu default filter� pasif hale getirdik kendi olu�turdu�umuz filter� d�nmesi i�in
////builder.Services.Configure<ApiBehaviorOptions>(options =>
////{
////    options.SuppressModelStateInvalidFilter = true;
////});
////builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateUserValidation));
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

////app.Use(async (context, next) =>
////{
////    // IsAuthenticated alan�nda null kontrol� yapt���m�z i�in Identity Name de null kontrol� yapmam�za gerek yok
////    var user = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity : null;    
////    LogContext.PushProperty("user",user);
////    await next();
////});