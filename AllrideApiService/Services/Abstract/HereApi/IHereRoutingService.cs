
using AllrideApiCore.Dtos.Here;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.HereApi
{
    public interface IHereRoutingService
    {
        Task<CustomResponse<HereDirectRequestResponseDto>> SendRequestHere(Dictionary<string, string> param,int UserId);  //Task<string>    HereRoute hereRoute
        Task<CustomResponse<object>> HereRequestForLiveRoute(Dictionary<string, string> param);
        //Task<CustomResponse<HereDirectRequestResponseDto>> HereDirectRequest(Dictionary<string, string> param);
        //Task<CustomResponse<string>> HereDirectRequest(Dictionary<string, string> param);


    }
}
