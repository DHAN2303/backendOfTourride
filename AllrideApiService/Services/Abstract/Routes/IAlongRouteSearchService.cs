using AllrideApiCore.Dtos.TomTom;

namespace AllrideApiService.Services.Abstract.Routes
{
    public interface IAlongRouteSearchService
    {
        // string RouteNearbyPoi(Dictionary<string, string> routingParameters);  //Dictionary<string,string> routingParameters
        Task<AlongRoot> CreateAlongRouteSearchService(string Latlong, Dictionary<string, dynamic> requestHeader);
    }
}
