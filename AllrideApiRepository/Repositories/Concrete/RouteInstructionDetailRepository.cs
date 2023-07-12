using AllrideApiCore.Entities.Here;
using AllrideApiRepository.Repositories.Abstract;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class RouteInstructionDetailRepository : IRouteInstructionDetailRepository
    {
        private readonly AllrideApiDbContext _dbContext;
        public RouteInstructionDetailRepository(AllrideApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(RouteInstructionDetail routeInstructionDetail)
        {
            _dbContext.route_instruction_detail.Add(routeInstructionDetail);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
