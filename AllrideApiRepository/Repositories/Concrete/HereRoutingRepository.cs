using AllrideApiCore.Entities.Here;
using AllrideApiRepository.Repositories.Abstract;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class HereRoutingRepository : IHereRoutingRepository
    {
        protected readonly AllrideApiDbContext _context;
        public HereRoutingRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public Route Add(Route route)
        {
            return _context.route.Add(route).Entity;
        }
        public void AddRouteInstruction(RouteInstruction routeInstruction)
        {
            _context.route_instruction.Add(routeInstruction);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Route GetLastByRoute()
        {
           //return  _context.route.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0;
           return  _context.route.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
