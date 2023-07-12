using AllrideApiCore.Entities.Buys;

namespace AllrideApiRepository.Repositories.Abstract.Buys
{
    public interface IBuyRepository
    {
        public List<TouridePackage> GetTouridePackageList();
    }
}
