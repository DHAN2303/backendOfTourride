using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Global
{
    [AllowHttps]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
