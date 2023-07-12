using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiService.Response;
using DTO.Insert;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface IUserService
    {
        CustomResponse<NoContentDto> Add(CreateUserDto user);
        CustomResponse<NoContentDto> UpdateUserVehicleType(string VehicleType, int UserID);
        public CustomResponse<Object> GetOnlineUsers(int type, int id, int userId);
        CustomResponse<List<UserProfileResponseDto>> GetFollowers(int UserId);
        CustomResponse<List<UserProfileResponseDto>> GetFollowing(int UserId);

        //UserDetail Update(UserDetail user);
        //void Delete(UserDetail user);
        //void SaveChanges();
    }
}
