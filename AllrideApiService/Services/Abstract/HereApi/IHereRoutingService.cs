
using AllrideApiCore.Dtos.Here;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.HereApi
{
    public interface IHereRoutingService
    {
        Task<CustomResponse<HereUIResultResponseDto>> SendRequestHere(Dictionary<string, string> param);  //Task<string>    HereRoute hereRoute
    }
}
