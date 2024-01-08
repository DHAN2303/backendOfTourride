using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Clubs;
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
        public CustomResponse<Object> GetClubUserDetail(int ClubId);
        CustomResponse<List<string>> SearchUserClub(string userName, int ClubId);
        public CustomResponse<Object> GetMemberedClubsByUser(int userId);
        public CustomResponse<Object> GetClubMessage(int clubId);

    }
}

