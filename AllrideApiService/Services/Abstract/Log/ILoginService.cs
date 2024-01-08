using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Dtos.Update;
using AllrideApiCore.Entities.Users;
using AllrideApiService.Response;
using DTO.Select;

namespace AllrideApiService.Services.Abstract.Log
{
    public interface ILoginService
    {
        public CustomResponse<UserEntity> VerifyPassword(LoginUserDto userDto);
        public CustomResponse<LoginUserResponseDto> Login(LoginUserDto userDto);
        public UserEntity GetUserById(int userId);
        public CustomResponse<UserUpdateDto> ForgotPassword(ForgotPasswordDto userUpdateDto);
    }
}
