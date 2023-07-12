using AllrideApiCore.Dtos.ResponseDto;

namespace AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats
{
    public interface IClubChatRepository
    {
        List<ClubChatListResponseDto> GetUserClubsLastMessage(int UserId);
    }
}
