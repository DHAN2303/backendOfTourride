using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Clubs;

namespace AllrideApiRepository.Repositories.Abstract.Clubs
{
    public interface IClubRepository
    {
        Club GetMedia(int clubId);
        ClubResponseDto GetClubDetail(int ClubId);
        List<GlobalClubResponseDto> GetGlobalClubs(int ClubId, int Type);
        bool DeleteClub(int ClubId);
        bool IsExistClub(int ClubId);
        bool DeleteUserInClub(int ClubId, int UserId);
        bool IsExistUserInClub(int ClubId, int UserId);
        UserResponseDto GetClubUserDetail(int ClubId);
        List<string> SearchUserClub(string userName, int GroupId);
        List<Club> SearchClub(string clubName);
        LastActivityResponseDto GetLastActivity(int ClubId);
        List<ClubResponseDto> GetUsersClubList(int userId);
        public List<ClubSocialMediaPostsResponseDto> GetClubUsersSocialMediaLast3Post(int clubId);
        public int GetClubMemberCount(int ClubId);
        public List<UserProfileResponseDto> GetClubsUsers(int ClubId);
    }
}
