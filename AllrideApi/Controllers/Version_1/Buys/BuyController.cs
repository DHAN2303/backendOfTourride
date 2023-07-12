using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Buys;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.Buys
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IBuyService _buyService;
        private readonly ILogger<BuyController> _logger;
        public BuyController(IBuyService buyService, ILogger<BuyController> logger)
        {
            _buyService = buyService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var touridePackageData = _buyService.GetTouridePackage();
                if (touridePackageData.Status == false)
                {
                    return StatusCode(500, touridePackageData);
                }
                return Ok(touridePackageData);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " BuyController  -->  Get METHOD  ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
    }
}
