using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.UserMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.UserMessage
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userMessagesRepository;

        private readonly ILogger<UserMessageService> _logger;
        public UserMessageService(ILogger<UserMessageService> logger,
            IHttpContextAccessor httpContextAccessor, IUserRepository userMessagesRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userMessagesRepository = userMessagesRepository;
            _logger = logger;
        }

        // NOT: Gelen UserId nin tokendan alınması önemli
        public CustomResponse<List<Message>> GetUserFriendsMessages(int UserId) //Claim userIdClaim
        {
            List<ErrorEnumResponse> _enumErrorResponse = new();
            List<Message> _listMessage = new();
            //if (userIdClaim == null)
            //{
            //    _enumErrorResponse.Add(ErrorEnumResponse.TokenIsInValid);
            //    return CustomResponse<List<Message>>.Fail(_enumErrorResponse, false);
            //}
            //var userId = userIdClaim.Value;
            //// UserId'yi kullanarak yapmak istediğiniz işlemler
            //bool isUserIdTypeInt = int.TryParse(userId, out int UserId);
            try
            {
                //if (isUserIdTypeInt == false)
                //{
                //    _enumErrorResponse.Add(ErrorEnumResponse.UserIdNotFound);
                //    return CustomResponse<List<Message>>.Fail(_enumErrorResponse, false);
                //}
                _listMessage = _userMessagesRepository.GetUserFriendsLastMessage(UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError("HAS ERROR USER MESSAGE SERVICE IN GetUserFriendsMessages METOD: " + ex.Message);
            }

            return CustomResponse<List<Message>>.Success(_listMessage, true);
        }

        public CustomResponse<Object> GetUserMessages(int userId, int clientId)
        {
            List<Message> _lastMessages = new();
            List<UserDetail> _userInfoList = new();
            try
            {
                _lastMessages = _userMessagesRepository.GetPeerToPeerMessage(userId, clientId);
                _userInfoList = _userMessagesRepository.GetMessagedUser(userId).ToList();

                var data = new
                {
                    lastMessage = _lastMessages,
                    usersData = _userInfoList
                };

                return new CustomResponse<Object>
                {
                    Data = data,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("HAS ERROR USER MESSAGE SERVICE IN GetUserMessages METOD: " + ex.Message);
            }
            return new CustomResponse<Object>
            {
                Data = null,
                Status = false
            };
        }

    }
}
