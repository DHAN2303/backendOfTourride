using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.Routes
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoutePlannerController : ControllerBase
    {
        private readonly IRoutePlannerService _routePlannerService;
        private readonly ILogger<RoutePlannerController> _logger;
        public RoutePlannerController(IRoutePlannerService routePlannerService, ILogger<RoutePlannerController> logger)
        {
            _routePlannerService = routePlannerService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public IActionResult SaveRoutePlanner([FromBody] CreateRoutePlannerDto routePlanner)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.SaveRoutePlanner(routePlanner, UserId);
                if (response.Status == false)
                {
                    return StatusCode(500, response);
                }
                else
                {
                    //return CreatedAtRoute("Create Route Planner", response);
                    return Ok( response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        // Rotaya Arkadaş Ekleme Servisi
        [HttpPost("AddFriends")]
        public IActionResult AddFriendsRoutePlanner([FromBody] AddFriendsRoutePlannerDto addFriendsRoutePlanner)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.AddFriendsRoutePlanner(addFriendsRoutePlanner, UserId);
                if (response.Status)
                {
                    return StatusCode(201, response);
                }
                else
                {
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }                        

        // Rota Ekibine Görev Atama
        [HttpPost("AssigningTasksToUsers")]
        public IActionResult AssigningTasksToUsersOnARoute([FromBody] AddFriendsTasksRoutePlannerDto addFriendsTasksRoutePlanner)
        {

            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.AssigningTasksToUsersOnARoute(addFriendsTasksRoutePlanner, UserId);
                if (response.Status == false)
                {
                    return StatusCode(500, response);
                }
                else
                {

                    return CreatedAtRoute(201, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpPost("AddTasks")]
        public IActionResult AddTasks(CreateTasksInRoutePlanner createTasks)
        {

            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.AddTasks(createTasks, UserId);
                if (response.Status == false)
                {
                    return StatusCode(500, response);
                }
                else
                {
                    return StatusCode(201, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpDelete("delete")]
        public IActionResult DeleteRoutePlanner(RoutePlannerDto routePlannerDto)
        {

            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.DeleteRoutePlanner(routePlannerDto.RoutePlannerId, UserId);
                if (response.Status == false)
                {
                    return StatusCode(500, response);
                }
                else
                {
                    return CreatedAtRoute(200, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpDelete("userLeaving")]
        public IActionResult DeleteUserForRoutePlanner([FromBody] RoutePlannerDto routePlannerDto)
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var response = _routePlannerService.UserLeaving(routePlannerDto.RoutePlannerId, UserId);
                if (response.Status == false)
                {
                    return StatusCode(500, response);
                }
                else
                {
                    return StatusCode(200, response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }

        }

        [HttpGet("allRoutePlanner")]
        public IActionResult GetAllRoutePlanner()
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var result  = _routePlannerService.GetAllRoutePlanner(UserId);
                if (result.Status == false)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpDelete("deleteTask")]
        public IActionResult DeleteTaskInRoutePlanner([FromBody] DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto )
        {
            var userId = HttpContext.User.Claims.First()?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (isUserIdTypeInt == false)
            {
                return Unauthorized();
            }
            try
            {
                var result = _routePlannerService.DeleteTaskFromRoutePlanner(deleteTaskRoutePlannerDto);
                if (result.Status == false)
                {
                    return StatusCode(500, result);
                }
                else
                {
                    return StatusCode(200, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }


        //[HttpPost("createForGroup")]
        //public IActionResult CreateGroupRoutePlanner(CreateRoutePlannerDto createRoutePlannerDto)
        //{
        //    var userId = HttpContext.User.Claims.First()?.Value;
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return Unauthorized();
        //    }
        //    bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

        //    if (isUserIdTypeInt == false)
        //    {
        //        return Unauthorized();
        //    }
        //    try
        //    {
        //        var response = _routePlannerService.SaveRoutePlanner(routePlanner, UserId);
        //        if (response.Status == false)
        //        {
        //            return StatusCode(500, response);
        //        }
        //        else
        //        {
        //            //return CreatedAtRoute("Create Route Planner", response);
        //            return Ok(response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("RoutePlannerController Create Route Planner AddFriendsRoutePlanner METHOD Error  Message: " + ex.Message);
        //        return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
        //    }
        //}


        // Testleri yapılacak

        // Buradaki bütün Post Servislerin Get leri yazılacak

        // Route Planner da Arama Yapacak

        // Grup Rota Oluşturma 

        // Club Rota Oluşturma

    }
}
