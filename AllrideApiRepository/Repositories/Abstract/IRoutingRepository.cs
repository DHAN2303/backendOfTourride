using AllrideApiCore.Dtos.RoutesDtos;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Routes;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IRoutingRepository
    {
        RouteCalculate Add(List<List<RoutesEntities>> pointList);
        RouteCalculate Update(RouteCalculate route);
        void SaveChanges();
        List<Route> GetLast3Routes(int UserId);
        List<int> GetRecommendedRoute(int recommendedType);
        List<RouteDetail> GetRecommendedRouteDetail(int recommendedType);
    }
}
