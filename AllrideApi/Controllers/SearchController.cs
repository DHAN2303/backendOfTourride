using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService= searchService;
        }
        [HttpGet("searchAllClubList")]
        public IActionResult Search(string Keyword)
        {
            var response = _searchService.GetClub(Keyword);
            if (response.Status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500,response);
            }
        }
    }
}
