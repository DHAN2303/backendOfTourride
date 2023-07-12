using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.ClubsInfo
{
    public interface IClubService
    {
        CustomResponse<ClubResponseDto> GetClubMedia(int ClubId);
        CustomResponse<ClubResponseDto> GetClubDetail(int ClubId);
        CustomResponse<GlobalClubResponseDto> GetGlobalClubs(int ClubId, int Type);
        CustomResponse<GlobalClubResponseDto> DeleteClub(int ClubId);
        CustomResponse<GlobalClubResponseDto> DeleteUserInClub(int ClubId, int UserId);
        CustomResponse<UserResponseDto> GetClubUserDetail(int ClubId);
        CustomResponse<List<string>> SearchUserClub(string userName, int ClubId);
        CustomResponse<LastActivityResponseDto> GetLastActivity(int ClubId);
        CustomResponse<List<ClubResponseDto>> GetUsersClubList(int userId);
        CustomResponse<List<ClubSocialMediaPostsResponseDto>> GetClubsUsersSocialMediaLast3Post(int groupId);
        CustomResponse<int> GetClubAdminNumber(int ClubId);
        CustomResponse<List<UserProfileResponseDto>> GetFollowers(int groupId);
    }
}

