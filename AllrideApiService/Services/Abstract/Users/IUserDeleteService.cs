using DTO.Select;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface IUserDeleteService
    {
        public object UserDelete(LoginUserDto userDto);
    }
}
