using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;

namespace AllrideApiRepository.Repositories.Abstract.GroupsClubs
{
    public interface IGroupRepository 
    {
         List<GroupResponseDto> GetMedia(int GroupId);
         GroupResponseDto GetGroupDetail(int GroupId); 
         List<GlobalGroupResponseDto> GetGlobalGroups(int GroupId, int Type);
         bool DeleteGroup(int GroupId);
         bool IsExistGroup(int GroupId);
         bool DeleteUserInGroup(int GroupId, int UserId);
         bool IsExistUserInGroup(int GroupId, int UserId);
        List<UserResponseDto> GetGroupUserDetail(int GroupId);
         List<string> SearchUserGroup(string userName, int GroupId);
        List<Group> SearchGroup(string groupName);
        public List<GroupMessage> GetGroupMessage(int groupId);
        public List<Group> GetGroupsForUser(int userId);
    }
}
