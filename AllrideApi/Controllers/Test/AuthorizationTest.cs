using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.test
{
    [NonController]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationTest : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello Touride";
        }
    }
}
