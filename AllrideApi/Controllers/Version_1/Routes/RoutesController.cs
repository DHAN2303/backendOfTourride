using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route=AllrideApiCore.Entities.Here.Route;
using AllrideApiService.Response;
using Nest;
using System.Security.Claims;

namespace AllrideApi.Controllers.Version_1.SocialMedia
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RoutesController : ControllerBase
    {
        private readonly IRoutesServices _routeServices;

        public RoutesController(IRoutesServices routeServices)
        {
            _routeServices = routeServices;
        }

        [HttpGet]
        [Route("myRoutes")]
        public async Task<IActionResult> myRoutes(string user_id)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (user_id == userId)
            {
                Task<List<Route>> response = _routeServices.fetchMyRoutes(user_id);
                if (response != null && response.IsCompletedSuccessfully)
                {
                    return Ok(CustomResponse<object>.Success(response, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }

        [HttpGet]
        [Route("publishedRoutes")]
        public async Task<IActionResult> publishedRoutes(string user_id)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (user_id == userId)
            {
                Task<List<Route>> response = _routeServices.fetchPublishedRoutes(user_id);
                if (response != null && response.IsCompletedSuccessfully)
                {
                    return Ok(CustomResponse<object>.Success(response, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }

        [HttpGet]
        [Route("favoriteRoutes")]
        public async Task<IActionResult> favoriteRoutes(string user_id)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (user_id == userId)
            {
                Task<List<Route>> response = _routeServices.fetchFavoriteRoutes(user_id);
                if (response != null && response.IsCompletedSuccessfully)
                {
                    return Ok(CustomResponse<object>.Success(response, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }

        [HttpGet("routeDetail")]
        public IActionResult GetRouteDetail([FromQuery]FetchUsersRouteRequestDto fetchUsersRouteDto)
        {
            var result = _routeServices.FetchUsersRoute(fetchUsersRouteDto);
            if (result.Status)
            {
                return Ok(CustomResponse<object>.Success(result, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }
        [HttpGet("editorialAdvice")]
        public IActionResult GetEditorsAdviceRoute(int recommendedType)
        {
            var result = _routeServices.GetRecommendedRoute(recommendedType);
            if (result.Status)
            {
                return Ok(CustomResponse<object>.Success(result.Data, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }
        }

        [HttpGet("userLastRoutes")]
        public IActionResult GetUserLast3Route()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            //// UserId'yi kullanarak yapmak istediğiniz işlemler
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }

            var result = _routeServices.GetRecommendedRoute(UserId);
            if (result.Status)
            {
                return Ok(CustomResponse<object>.Success(result.Data, true));
            }
            else
            {
                return StatusCode(500,CustomResponse<object>.Success(result.ErrorEnums, false));
            }
        }


    }
}



//var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
//var emailClaim = HttpContext.User.FindFirst(ClaimTypes.Email);
//if (userIdClaim == null || emailClaim == null)
//{
//    return BadRequest("Invalid token or user information not found");
//}

//var userId = userIdClaim.Value;
//bool success = int.TryParse(userId, out int uId);
//if (success)
//{
//    // The parse operation was successful.
//    // The integer value is stored in the x variable.

//    var result = _routeServices.FetchUsersRoute(fetchUsersRouteDto, uId);
//    return result.Status == false ? BadRequest(result.ErrorEnums) : Ok(result.Data);
//}
//else
//{
//    return BadRequest("Invalid token or user information not found");
//}