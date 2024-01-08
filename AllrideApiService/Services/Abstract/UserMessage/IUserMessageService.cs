using AllrideApiCore.Entities.Chat;
using AllrideApiService.Response;
using System.Security.Claims;

namespace AllrideApiService.Services.Abstract.UserMessage
{
    public interface IUserMessageService
    {
        public CustomResponse<List<Message>> GetUserFriendsMessages(int UserId); //Claim userId
        public CustomResponse<Object> GetUserMessages(int userId, int clientId);

    }
}
