using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Chats.GroupChats
{
    public interface IClubChatService
    {
        CustomResponse<List<ClubChatListResponseDto>> GetUserClubMessages(int UserId);
    }
}
