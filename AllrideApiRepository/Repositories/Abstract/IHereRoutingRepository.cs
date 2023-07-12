using AllrideApiCore.Entities.Here;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IHereRoutingRepository
    {
        public Route Add(Route route);
        public void AddRouteInstruction(RouteInstruction routeInstruction);
        public Route GetLastByRoute();
        public void SaveChanges();
    }
}
