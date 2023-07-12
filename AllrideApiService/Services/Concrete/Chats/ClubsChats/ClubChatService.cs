using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Chats.GroupChats;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Chats.GroupsChats
{
    // 29 MAYIS
    public class ClubChatService : IClubChatService
    {
        private readonly IClubChatRepository _clubChatRepository;
        private readonly ILogger<ClubChatService> _logger;
        public ClubChatService(IClubChatRepository clubChatRepository, ILogger<ClubChatService> logger)
        {
            _clubChatRepository = clubChatRepository;
            _logger= logger;
        }

        public CustomResponse<List<ClubChatListResponseDto>> GetUserClubMessages(int UserId)
        {
            List<ErrorEnumResponse> _enumErrorResponse = new();
            List<ClubChatListResponseDto> _listMessage = new();

            try
            {
                _listMessage = _clubChatRepository.GetUserClubsLastMessage(UserId);
                if (_listMessage == null)
                {
                    _enumErrorResponse.Add(ErrorEnumResponse.ClubMessageListNull);

                    return CustomResponse<List<ClubChatListResponseDto>>.Fail(_enumErrorResponse, true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("HAS ERROR USER MESSAGE SERVICE IN GetUserClubsLastMessage METOD: " + ex.Message);
            }

            return CustomResponse<List<ClubChatListResponseDto>>.Success(_listMessage, true);
        }

    }
}
