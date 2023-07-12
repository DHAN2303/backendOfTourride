using AllrideApiCore.Entities.Buys;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Buys
{
    public interface IBuyService
    {
        public CustomResponse<List<TouridePackage>> GetTouridePackage();
    }
}
