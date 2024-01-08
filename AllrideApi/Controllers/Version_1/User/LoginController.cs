using AllrideApi.Controllers.Version_1.Chat.Group;
using AllrideApiCore.Dtos.Update;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Log;
using DTO.Select;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _authService;
        private readonly ILogger<GroupsController> _logger;
        public LoginController(ILoginService authService, ILogger<GroupsController> logger)
        {
            _authService = authService;
            _logger = logger;

        }
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUser)  // daha sonra burası from body olacak
        {
            var response = _authService.Login(loginUser);
            List<ErrorEnumResponse> errors = new();
            if(response.ErrorEnums != null && response.Status == false)
            {
                foreach (ErrorEnumResponse error in response.ErrorEnums)
                {
                    _logger.LogError(error.ToString());
                    if (error == ErrorEnumResponse.FailedToCreateToken || error == ErrorEnumResponse.TokenIsInValid)
                    {
                        errors.Add(error);
                    }
                }
               return Ok(CustomResponse<object>.Success(errors, false));
            }
            return Ok(CustomResponse<object>.Success(response.Data, true));
        }

        [HttpPut]
        [Route("forgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordDto userUpdateDto)
        {
            var response = _authService.ForgotPassword(userUpdateDto);
            if (response.Status)
            {
               return Ok(CustomResponse<object>.Success(response, true));
            }
            else
            {
               return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
            }
        }

    }
}
