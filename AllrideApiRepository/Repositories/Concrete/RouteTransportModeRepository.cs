using AllrideApiCore.Entities.Here;
using AllrideApiRepository.Repositories.Abstract;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class RouteTransportModeRepository : IRouteTransportModeRepository
    {
        private readonly AllrideApiDbContext _context;
        public RouteTransportModeRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public RouteTransportMode Get(string mode)
        {
           return _context.route_transport_mode.SingleOrDefault(x=> x.Mode == mode);
        }
    }
}
