
using AllrideApiCore.Entities.Clubs;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.GroupsInfo
{
    public interface IClubSettingsService
    {
        public CustomResponse<string> UpdateProfileImage(int clubId, int userId, string path);
        public CustomResponse<string> UpdateBackgroundCoverImage(int clubId, int userId,string path);
        public CustomResponse<string> UpdateClubName(int clubId, int userId, string newClubName);
        public CustomResponse<string> AddUser(int clubId, int userId);
        public CustomResponse<List<ClubMember>> DeleteUser(int clubId, int userId, List<int> memberId);
    }
}
