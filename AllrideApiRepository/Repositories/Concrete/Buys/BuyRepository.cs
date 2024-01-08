using AllrideApiCore.Entities.Buys;
using AllrideApiRepository.Repositories.Abstract.Buys;

namespace AllrideApiRepository.Repositories.Concrete.Buys
{
    public class BuyRepository : IBuyRepository
    {
        private readonly AllrideApiDbContext _context;
        public BuyRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public List<TouridePackage> GetTouridePackageList()
        {
            return  _context.touridePackage.ToList();

        }
    }
}
