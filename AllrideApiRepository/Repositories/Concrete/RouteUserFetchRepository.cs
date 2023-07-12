using AllrideApiCore.Entities.Routes;
using AllrideApiRepository.Repositories.Abstract;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class RouteUserFetchRepository : IRouteUserFetchRepository
    {

        protected readonly AllrideApiDbContext _context;
        public RouteUserFetchRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public RouteDetail GetRouteDetail(int id)
        {
            return _context.route_detail.Find(id);
        }
        public RouteDetail GetUsersRouteDetail(int UserId, int RouteId)
        {
            return _context.route
             .Where(r => r.UserId == UserId && r.Id == RouteId)
             .Select(r => r.RouteDetail).SingleOrDefault();

        }
    }
}
