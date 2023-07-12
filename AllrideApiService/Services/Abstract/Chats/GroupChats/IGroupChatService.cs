using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Chats.GroupChats
{
    public interface IGroupChatService
    {
        CustomResponse<List<GroupChatListResponseDto>> GetUserGroupsMessages(int UserId);
    }
}
