using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.TomtomApi
{
    public interface ITomtomRoutingService
    {
        Task<RouteResponse> CreateRouteCalculatePost(Dictionary<string, string> parameters); //RouteCalculateRequestParameter _routeCalculRequestParam //string Latlong,
        Task<RouteResponse> CreateRouteCalculateGet(string Latlong, Dictionary<string, string> parameters);
    }
}
