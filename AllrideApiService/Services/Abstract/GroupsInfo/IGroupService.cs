using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;
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
        CustomResponse<Object> GetGroupUserDetail(int GroupId);
        CustomResponse<List<string>> SearchUserGroup(string userName, int GroupId);
        public CustomResponse<Object> GetMemberedGroupsByUser(int userId);
        public CustomResponse<Object> GetGroupMessage(int groupId);

    }
}
