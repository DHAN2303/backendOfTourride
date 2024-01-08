using AllrideApiService.Services.Abstract.Buys;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.Buys
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IBuyService _buyService;
        public BuyController(IBuyService buyService)
        {
            _buyService= buyService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var touridePackageData = _buyService.GetTouridePackage();
            if(touridePackageData.Status == false)
            {
                return StatusCode(500, touridePackageData.ErrorEnums);
            }
            return Ok(touridePackageData.Data);
        }
    }
}
