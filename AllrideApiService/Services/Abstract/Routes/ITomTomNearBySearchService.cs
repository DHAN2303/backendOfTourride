using AllrideApiCore.Dtos;

namespace AllrideApiService.Services.Abstract.Routes
{
    public interface ITomTomNearBySearchService
    {
        Task<TomTomNearbySearchResult> CreateNearBySearchService(Dictionary<string, dynamic> nearBySearchParam);

    }
}
