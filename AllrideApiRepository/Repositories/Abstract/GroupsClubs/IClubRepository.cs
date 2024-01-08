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
         List<UserResponseDto> GetClubUserDetail(int ClubId);
         List<string> SearchUserClub(string userName, int GroupId);
         List<Club> SearchClub(string clubName);
        public List<Club> GetClubsForUser(int userId);
        public List<ClubMessage> GetClubMessage(int clubId);

    }
}
