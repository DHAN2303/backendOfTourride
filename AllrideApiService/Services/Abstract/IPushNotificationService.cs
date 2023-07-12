

using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract
{
    public interface IPushNotificationService
    {
        public CustomResponse<Object> InviteGroup(int senderId, int receiveId, int groupId);
        public CustomResponse<Object> InviteClub(int senderId, int receiveId, int clubId);
        public CustomResponse<Boolean> InviteReply(int inviteId, bool inviteRep);
        public CustomResponse<Object> GetCanInviteUsers(int userId, int where, int whereId);

    }
}
