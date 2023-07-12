using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.GroupsInfo
{
    public interface IGroupService
    {
        CustomResponse<GroupResponseDto> GetGroupMedia(int GroupId);
        CustomResponse<GroupResponseDto> GetGroupDetail(int GroupId);
        CustomResponse<GlobalGroupResponseDto> GetGlobalGroups(int GroupId, int Type);
        CustomResponse<GlobalGroupResponseDto> DeleteGroup(int GroupId);
        CustomResponse<GlobalGroupResponseDto> DeleteUserInGroup(int GroupId, int UserId);
        CustomResponse<UserResponseDto> GetGroupUserDetail(int GroupId);
        CustomResponse<List<string>> SearchUserGroup(string userName, int GroupId);
        CustomResponse<LastActivityResponseDto> GetLastActivity(int GroupId);
        CustomResponse<List<GroupResponseDto>> GetUsersGroupList(int userId);
        CustomResponse<List<GroupSocialMediaPostsResponseDto>> GetGroupsUsersSocialMediaLast3Post(int groupId);
        CustomResponse<List<UserProfileResponseDto>> GetFollowers(int groupId);


    }
}
