using AllrideApiCore.Entities.Routes;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IRouteUserFetchRepository
    {
        public RouteDetail GetRouteDetail(int id);
    }
}
