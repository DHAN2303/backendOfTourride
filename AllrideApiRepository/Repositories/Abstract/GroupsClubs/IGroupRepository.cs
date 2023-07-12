
using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Groups;

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
        UserResponseDto GetGroupUserDetail(int GroupId);
        List<string> SearchUserGroup(string userName, int GroupId);
        List<Group> SearchGroup(string groupName);
        LastActivityResponseDto GetLastActivity(int GroupId);
        string UpdateProfileOrBacgroundImage(int groupId); //, char changeProfileOrBackground
        List<GroupResponseDto> GetUsersGroupList(int userId);
        public List<GroupSocialMediaPostsResponseDto> GetGroupsUsersSocialMediaLast3Post(int groupId);
        public List<UserProfileResponseDto> GetGroupsUsers(int GroupId);
        public GroupMember GetGroupMember(int GroupId);

    }
}
