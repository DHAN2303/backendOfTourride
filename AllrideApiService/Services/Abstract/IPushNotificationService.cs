

using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract
{
    public interface IPushNotificationService
    {
        public CustomResponse<Object> InviteGroup(int senderId, string receiveIds, int groupId);
        public CustomResponse<Object> InviteClub(int senderId, string receiveIds, int groupId);
        public CustomResponse<Boolean> InviteReply(int inviteId, bool inviteRep);
        public CustomResponse<Object> GetCanInviteUsers(int userId, int where, int whereId);

    }
}
