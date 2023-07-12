using AllrideApiCore.Dtos.Here;

namespace AllrideApiService.Services.Abstract.Routes
{
    public interface IHereNearBySearchService
    {
        Task<HereNearByRootobject> CreateHereNearBySearchService(Dictionary<string, dynamic> hereNearBySearchParam);

    }
}
