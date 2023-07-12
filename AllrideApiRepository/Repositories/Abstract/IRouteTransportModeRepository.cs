using AllrideApiCore.Entities.Here;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IRouteTransportModeRepository
    {
        public RouteTransportMode Get(string mode);

    }
}
