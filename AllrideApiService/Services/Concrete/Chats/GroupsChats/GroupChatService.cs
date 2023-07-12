using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Chats.GroupChats;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Chats.GroupsChats
{
    
    public class GroupChatService : IGroupChatService
    {
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly ILogger<ClubChatService> _logger;
        public GroupChatService(IGroupChatRepository groupChatRepository,ILogger<ClubChatService> logger)
        {
            _groupChatRepository= groupChatRepository;
            _logger= logger;
        }
        public CustomResponse<List<GroupChatListResponseDto>> GetUserGroupsMessages(int UserId)
        {
            List<ErrorEnumResponse> _enumErrorResponse = new();
            List<GroupChatListResponseDto> _listMessage = new();

            try
            {
                _listMessage = _groupChatRepository.GetUserGroupsLastMessage(UserId);
                if (_listMessage == null)
                {
                    _enumErrorResponse.Add(ErrorEnumResponse.GroupMessageListNull);

                    return CustomResponse<List<GroupChatListResponseDto>>.Fail(_enumErrorResponse, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("HAS ERROR USER MESSAGE SERVICE IN GetUserGroupsLastMessage METOD: " + ex.Message);
            }

            return CustomResponse<List<GroupChatListResponseDto>>.Success(_listMessage, true);
        }
    }
}
