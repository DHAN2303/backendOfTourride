using AllrideApiCore.Entities.Buys;
using AllrideApiRepository.Repositories.Abstract.Buys;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Buys;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Buys
{
    public class BuyService : IBuyService
    {
        private readonly IBuyRepository _buyRepository;
        private readonly ILogger<BuyService> _logger;
        public BuyService(IBuyRepository buyRepository, ILogger<BuyService> logger)
        {
            _buyRepository = buyRepository;
            _logger = logger;
        }

        public CustomResponse<List<TouridePackage>> GetTouridePackage()
        {
            List<TouridePackage> touridePackageList = new();
            try
            {
                touridePackageList = _buyRepository.GetTouridePackageList();
                List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
                if(touridePackageList == null)
                {
                    errors.Add(ErrorEnumResponse.NoTouridePackagePricingInDB);
                    return CustomResponse<List<TouridePackage>>.Fail(errors, false);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(" TOURIDE PACKAGE SERVICE ERROR : " + ex.Message, ex);
            }

            return CustomResponse<List<TouridePackage>>.Success(touridePackageList, true);
        }
    }
}
