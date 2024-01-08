using AllrideApiCore.Dtos.Select;
using AllrideApiCore.Entities.Users;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Users;
using DTO.Insert;
using DTO.Select;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllrideApi.Controllers.Version_1.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserDeleteService _userDeleteService;
        private readonly IUserUpdateService _userUpdateService;
        private readonly IUserGeneralService _userGeneralService;

        public UserController(IUserGeneralService userGeneralService, IUserService service, IUserDeleteService userDeleteService, 
            IUserUpdateService userUpdateService)
        {
            _userService = service;
            _userDeleteService = userDeleteService;
            _userUpdateService = userUpdateService;
            _userGeneralService = userGeneralService;
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] CreateUserDto request)
        {
            var response = _userService.Add(request);
            if (response != null)
            {
                return Ok(CustomResponse<object>.Success(response.Data, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }
        }


        [HttpDelete]
        [Route("deleteUser")]
        [Authorize]
        public IActionResult UserDelete([FromBody] LoginUserDto userDto)
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (userEmail == userDto.Email)
            {
                var response = _userDeleteService.UserDelete(userDto);
                if (response == null)
                {
                    return Ok(CustomResponse<object>.Success(response, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.EmailNotRegistered, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }

        [HttpPut]
        [Route("updateUser")]
        [Authorize]
        public IActionResult UserUpdate([FromBody] UserUpdate user)
        {
            var user_email = HttpContext.User.Claims.Last()?.Value;
            

            if (user_email == user.email)
            {
                var response = _userUpdateService.UpdateUser(user);
                if (response == null)
                {
                    return Ok(CustomResponse<object>.Success(response, true));
                }
                else
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.TokenIsInValid, false));
                }
            }
            else
            {
               return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }


        [HttpGet]
        [Route("fetchUser")]
        [Authorize]
        public IActionResult FetchUser()
        {
            var user_id = HttpContext.Request.Headers["id"];
            var response = _userUpdateService.FetcUserData(Convert.ToInt32(user_id));
            
            if (response != null)
            {
               return Ok(CustomResponse<object>.Success(response, true));
            }
            else
            {
               return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }

        [HttpPut("vehicleType")]
        public IActionResult SaveUserVehicleType([FromBody] string VehicleType) 
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);

            if (string.IsNullOrEmpty(userId) || isUserIdTypeInt == false)
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.TokenIsInValid, false));
            }

            var response = _userService.UpdateUserVehicleType(VehicleType, UserId);

            if (response.StatusCode == (int)ErrorEnumResponse.NewsIdNullOrEmpty)
            {
                return Ok(CustomResponse<object>.Success(response.Errors, false));
            }

            else if (response.Data == null && response.Status)
            {
                // Burada Data olarak ne dönülecek ??? 
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(response.ErrorEnums, false));
               // return StatusCode(500, response.ErrorEnums);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getAllUsers")]
        public ActionResult<IList<UserGeneralDto>> Get()
        {
            var response = _userGeneralService.GetAll();
            if (response.Status)
            {
                return Ok(CustomResponse<object>.Success(response, true));
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }

        }

        [HttpGet]
        [Route("getOnlineUsers")]
        public ActionResult GetOnlineUsers(int whereType, int whereId) 
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = _userService.GetOnlineUsers(whereType, whereId, Convert.ToInt32(userId));
            return Ok(response);
        }

    }
}
