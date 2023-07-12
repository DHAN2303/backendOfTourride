using AllrideApiCore.Entities.Groups;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.GroupsInfo
{
    public interface IGroupSettingsService
    {
        public CustomResponse<string> UpdateProfileImage(int groupId, int userId, string path);
        public CustomResponse<string> UpdateBackgroundCoverImage(int groupId, int userId,string path);
        public CustomResponse<string> UpdateGroupName(int groupId, int userId, string newGroupName);
        public CustomResponse<string> AddUser(int groupId, int userId);
        public CustomResponse<List<GroupMember>> DeleteUser(int groupId, int userId, List<int> memberId);
    }
}
