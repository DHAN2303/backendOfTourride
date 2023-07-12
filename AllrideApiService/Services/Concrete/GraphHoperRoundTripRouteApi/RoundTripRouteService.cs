using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.RequestDto.GraphHopper;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.GraphHoperRoundTripRouteApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.GraphHoperRoundTripRouteApi
{
    public class RoundTripRouteService : IRoundTripRouteService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<RoundTripRouteService> _logger;
        private readonly IRouteTransportModeRepository _routeTransportModeRepository;
        private readonly IRouteInstructionDetailRepository _routeInstructionDetailRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public RoundTripRouteService(IConfiguration config, HttpClient httpClient, 
            ILogger<RoundTripRouteService> logger,
            IRouteTransportModeRepository routeTransportModeRepository, 
            IRouteInstructionDetailRepository routeInstructionDetailRepository)
        {
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
            _routeTransportModeRepository = routeTransportModeRepository;
            _routeInstructionDetailRepository = routeInstructionDetailRepository;
        }

        //public async Task<CustomResponse<NoContentDto>> GetRoundTripRoute(CreateRoundTripRequestDto createRoundTripRequestDto, int UserId)
        //{

        //    List<ErrorEnumResponse> errors = new();
        //    try
        //    {               
        //        var client = _httpClientFactory.CreateClient();
        //        var request = await client
        //            .GetAsync("https://graphhopper.com/api/1/route?profile=car&point=11.539421%2C%2048.118477%2C11.559023%2C%2048.12228&point_hint=Lindenschmitstra%C3%9Fe%2CThalkirchener%20Str.&snap_prevention=string&curbside=any&locale=en&elevation=true&details=string&optimize=false&instructions=true&calc_points=true&debug=true&points_encoded=true&ch.disable=false&heading=0&heading_penalty=120&pass_through=true&algorithm=round_trip&round_trip.distance=10000&round_trip.seed=0&alternative_route.max_paths=2&alternative_route.max_weight_factor=1.4&alternative_route.max_share_factor=0.6&key=eac9f5db-7e05-4a65-85d3-c75c0cc03a1c%09");
        //        var response = request.Content.ReadAsStringAsync();
        //        if (!response.IsCompletedSuccessfully)
        //        {
        //            return CustomResponse<NoContentDto>.Fail(errors, false);
        //        }
        //        return CustomResponse<NoContentDto>.Success(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message + " RoundTripRouteService  -->  GetRounTripRoute METHOD  ERROR: " + ex.InnerException.ToString());
        //    }
        //}

    }
}

//var client = _httpClientFactory.CreateClient();
//var request = await client.GetAsync("https://graphhopper.com/api/1/route?profile=car&point=string&point_hint=string&snap_prevention=string&curbside=any&locale=en&elevation=false&details=string&optimize=false&instructions=true&calc_points=true&debug=false&points_encoded=true&ch.disable=false&heading=0&heading_penalty=120&pass_through=false&algorithm=round_trip&round_trip.distance=10000&round_trip.seed=0&alternative_route.max_paths=2&alternative_route.max_weight_factor=1.4&alternative_route.max_share_factor=0.6&key=Tglabsroundtrip33");
//var response = await request.Content.ReadAsStringAsync();