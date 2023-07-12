using AllrideApiCore.Dtos.ResponseDto;

namespace AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats
{
    public interface IGroupChatRepository
    {
        List<GroupChatListResponseDto> GetUserGroupsLastMessage(int UserId);
    }
}
