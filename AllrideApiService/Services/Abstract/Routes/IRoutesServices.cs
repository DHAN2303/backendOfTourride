using AllrideApiCore.Dtos.RequestDto;
using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Entities.Here;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Routes
{
    public interface IRoutesServices
    {
        public Task<List<Route>> fetchMyRoutes(string user_id);
        public Task<List<Route>> fetchPublishedRoutes(string user_id);
        public Task<List<Route>> fetchFavoriteRoutes(string user_id);
        //public CustomResponse<RouteDetailResponseDto> FetchUsersRouteAndRouteDetail(FetchUsersRouteRequestDto fetchUsersRouteDto, int UserId);
        public CustomResponse<RouteDetailResponseDto> FetchUsersRoute(FetchUsersRouteRequestDto fetchUsersRouteDto);
        public CustomResponse<List<RouteDetailResponseDto>> GetRecommendedRoute(int recommendedType);
    }
}
