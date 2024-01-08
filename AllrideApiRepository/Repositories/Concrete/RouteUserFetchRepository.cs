using AllrideApiCore.Entities.Routes;
using AllrideApiRepository.Repositories.Abstract;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class RouteUserFetchRepository : IRouteUserFetchRepository
    {
        private readonly AllrideApiDbContext _context;
        public RouteUserFetchRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public RouteDetail GetRouteDetail(int id)
        {
          return _context.route_detail.Find(id);
          //return _context.route_detail.SingleOrDefault(x => x.Id == id);
        }
    }
}
