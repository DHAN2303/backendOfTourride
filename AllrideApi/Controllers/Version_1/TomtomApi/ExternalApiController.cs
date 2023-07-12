using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.TomtomApi
{
    [NonController]
    [Route("api/[controller]s")]
    [ApiController]
    public class ExternalApiRequest : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IHttpClientFactory factory;
        public ExternalApiRequest(IHttpClientFactory factory, IExternalApiService externalApiService)
        {
            this.factory = factory;
            _externalApiService = externalApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoute(string url)
        {
            var response = await _externalApiService.Get(url);
            return Ok(CustomResponse<object>.Success(response, true));
        }


    }
}
