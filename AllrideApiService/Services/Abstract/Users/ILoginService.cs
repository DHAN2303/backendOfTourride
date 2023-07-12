using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Dtos.Update;
using AllrideApiCore.Entities.Users;
using AllrideApiService.Response;
using DTO.Select;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface ILoginService
    {
        public CustomResponse<UserEntity> VerifyPassword(LoginUserDto userDto);
        public CustomResponse<LoginUserResponseDto> Login(LoginUserDto userDto);
        public Task<CustomResponse<NoContentDto>> SendForgotPasswordMail(string UserMail);
        CustomResponse<object> VerifyResetCode(ForgotPasswordDto userUpdateDto);
        CustomResponse<NoContentDto> UserPasswordReset(CreateUserResetPasswordDto createUserResetPasswordDto);
        Task<CustomResponse<UserUpdateDto>> ForgotPassword(ForgotPasswordDto userUpdateDto);
        public UserEntity GetUserById(int Id);

    }
}
