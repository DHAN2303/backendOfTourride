using AllrideApiCore.Entities.Here;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IRouteInstructionDetailRepository
    {
        public void Add(RouteInstructionDetail routeInstructionDetail);
        public void Save();

    }
}
