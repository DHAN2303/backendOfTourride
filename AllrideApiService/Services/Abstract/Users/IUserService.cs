using AllrideApiCore.Dtos;
using AllrideApiService.Response;
using DTO.Insert;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface IUserService
    {
        CustomResponse<NoContentDto> Add(CreateUserDto user);
        CustomResponse<NoContentDto> UpdateUserVehicleType(string VehicleType, int UserID);
        public CustomResponse<Object> GetOnlineUsers(int type, int id, int userId);

        //UserDetail Update(UserDetail user);
        //void Delete(UserDetail user);
        //void SaveChanges();
    }
}
