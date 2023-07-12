
using AllrideApiCore.Entities.Routes;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IRouteUserFetchRepository
    {
        public RouteDetail GetUsersRouteDetail(int UserId, int RouteId);
        public RouteDetail GetRouteDetail(int id);
    }
}
