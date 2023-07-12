using AllrideApi.Hubs;
using AllrideApiCore.Entities.Clubs;
using AllrideApiCore.Entities.Groups;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Concrete.UserCommon;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Notification
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IHubContext<ChatHubs> _hubContext;
        protected readonly AllrideApiDbContext _context;
        private readonly ILogger<LoginService> _logger;


        public PushNotificationService(IHubContext<ChatHubs> hubContext, AllrideApiDbContext context, ILogger<LoginService> logger)
        {
            _hubContext = hubContext;
            _context = context;
            _logger = logger;
        }

        public CustomResponse<Object> GetCanInviteUsers(int userId, int where, int whereId)
        {
            try
            {
                if (where == 1)
                {
                    var friendsToInvite = from follow in _context.social_media_follows
                                          join member in _context.group_member
                                          on new { FriendId = follow.followed_id, GroupId = whereId } equals new { FriendId = member.user_id, GroupId = member.group_id } into gj
                                          from submember in gj.DefaultIfEmpty()
                                          where follow.follower_id == userId && submember == null
                                          select follow.followed_id;

                    return CustomResponse<object>.Success(friendsToInvite, true);
                }
                else
                {
                    var friendsToInvite = from follow in _context.social_media_follows
                                          join member in _context.club_member
                                          on new { FriendId = follow.followed_id, GroupId = whereId } equals new { FriendId = member.user_id, GroupId = member.club_id } into gj
                                          from submember in gj.DefaultIfEmpty()
                                          where follow.follower_id == userId && submember == null
                                          select follow.followed_id;

                    return CustomResponse<object>.Success(friendsToInvite, true);
                }
            }
            catch(Exception ex)
            {
                if(where == 0)
                    _logger.LogError("Where invite to group an error occurred: " + ex.Message + " " + ex.InnerException);
                else
                    _logger.LogError("Where invite to club an error occurred: " + ex.Message + " " + ex.InnerException);

                return CustomResponse<object>.Success(false);
            }


        }

        public CustomResponse<object> InviteGroup(int senderId, int receiveId, int groupId)
        {
            try
            {
                var newInvite = new UserInvites
                {
                    inveting_id = senderId,
                    invited_id = receiveId,
                    where = 0,
                    status = false,
                    invitedDateTime = DateTime.UtcNow
                };
                _context.user_invites.Add(newInvite);
                _context.SaveChanges();

                var group = _context.groups.Where(b => b.id == groupId).First();
                var inviteId = _context.user_invites.First().Id;

                _hubContext.Clients.User(receiveId.ToString()).SendAsync("ReceiveGroupInviteNotification", group, inviteId);

                return CustomResponse<object>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured when user with " + senderId + " id is trying to invite user with " + groupId + " id to group with " + receiveId + " id. Error is " + ex.Message + " " + ex.InnerException);
                return CustomResponse<object>.Success(false);
            }
        }

        public CustomResponse<object> InviteClub(int senderId, int receiveId, int clubId)
        {
            try
            {
                var newInvite = new UserInvites
                {
                    inveting_id = senderId,
                    invited_id = receiveId,
                    where = 0,
                    status = false,
                    invitedDateTime = DateTime.UtcNow
                };
                _context.user_invites.Add(newInvite);
                _context.SaveChanges();

                var club = _context.club.Where(b => b.Id == clubId).First();
                var inviteId = _context.user_invites.First().Id;

                _hubContext.Clients.User(receiveId.ToString()).SendAsync("ReceiveGroupInviteNotification", club, inviteId);

                return CustomResponse<object>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured when user with " + senderId + " id is trying to invite user with " + clubId + " id to group with " + receiveId + " id. Error is " + ex.Message + " " + ex.InnerException);
                return CustomResponse<object>.Success(false);
            }
        }

        public CustomResponse<bool> InviteReply(int inviteId, bool inviteRep)
        {
            var invite = _context.user_invites.Where(b => b.Id == inviteId).First();

            if (invite != null)
            {
                if (inviteRep)
                {
                    var where = invite.where;
                    var whereId = invite.whereId;
                    if (where == 0)
                    {
                        var newGroupMember = new GroupMember
                        {
                            group_id = whereId,
                            user_id = invite.invited_id,
                            role = 2,
                            joined_date = DateTime.UtcNow
                        };
                        _context.group_member.Add(newGroupMember);

                        return CustomResponse<bool>.Success(true);
                    }
                    else
                    {
                        var newClubMember = new ClubMember
                        {
                            club_id = whereId,
                            user_id = invite.invited_id,
                            role = 2,
                            joined_date = DateTime.UtcNow
                        };
                        _context.club_member.Add(newClubMember);
                    }
                }
                else
                {
                    _context.user_invites.Remove(invite);
                }

                _context.SaveChanges();
                return CustomResponse<bool>.Success(true);
            }
            else
            {
                return CustomResponse<bool>.Success(false);
            }
        }
    }
}
