using AllrideApi.Controllers.Version_1.ClubsController;
using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.Update;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Mail;
using AllrideApiService.Services.Abstract.Users;
using DTO.Select;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Version_1.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _authService;
        private readonly ILogger<ClubSettingsController> _logger;
        private readonly IMailService _mailService;
        public LoginController(ILoginService authService, ILogger<ClubSettingsController> logger, IMailService mailService)
        {
            _authService = authService;
            _logger = logger;
            _mailService = mailService;

        }
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUser)  // daha sonra burası from body olacak
        {
            try
            {
                var response = _authService.Login(loginUser);
                List<ErrorEnumResponse> errors = new();
                if (response.ErrorEnums != null && response.Status == false)
                {
                    foreach (ErrorEnumResponse error in response.ErrorEnums)
                    {
                        _logger.LogError(error.ToString());
                        if (error == ErrorEnumResponse.FailedToCreateToken || error == ErrorEnumResponse.TokenIsInValid)
                        {
                            errors.Add(error);
                        }
                    }

                    return StatusCode(500, response);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(" LOG CONTROLLER LOGIN METHOD ERROR " + ex.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }

        [HttpPut]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto userUpdateDto)
        {
            try
            {
                var response = await _authService.ForgotPassword(userUpdateDto);
                if (response.Status)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  LoginController --> ForgotPassword  METHOD ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);
            }
        }
        [HttpPost("checkUserForgotPasswordCode")]
        public IActionResult CheckUserResetCode([FromBody] ForgotPasswordDto userUpdateDto)
        {
            try
            {
                var response = _authService.VerifyResetCode(userUpdateDto);
                if (response.Status)
                {
                    return Ok(response.Status);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  LoginController --> CheckUserResetCode  METHOD ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);

            }
        }

        [HttpPut("userPasswordReset")]
        public IActionResult UserPasswordReset([FromBody] CreateUserResetPasswordDto createUserResetPasswordDto)
        {
            try
            {
                var response = _authService.UserPasswordReset(createUserResetPasswordDto);
                if (response.Status)
                {
                    return Ok(response.Status);
                }
                else
                {
                    return StatusCode(500, response.ErrorEnums);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  LoginController --> UserPasswordReset  METHOD ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);

            }
        }

        [HttpGet("sendMail")]
        public async Task<IActionResult> ExampleMailTest(string mail)
        {
            try
            {
                await _mailService.SendMessageAsync(mail, "aktivasyon kodunuz", "<b> Bu bir test mailidir <b>");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "  LoginController --> ExampleMailTest  METHOD ERROR: " + ex.InnerException.ToString());
                return StatusCode(500, ErrorEnumResponse.ApiServiceFail);

            }
        }

    }
}
