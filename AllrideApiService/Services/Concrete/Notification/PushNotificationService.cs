using AllrideApi.Hubs;
using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Chat.Clubs;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Concrete.UserCommon;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CustomResponse<Object> GetCanInviteUsers(int userId, int where, int whereId) // 0: group || 2: club || 1: new creatin club or group
        {
            try
            {
                if (where == 0) 
                {
                    var friendsToInvite = from follow in _context.social_media_follows
                                          join member in _context.group_member
                                          on new { FriendId = follow.follower_id, GroupId = whereId } equals new { FriendId = member.user_id, GroupId = member.group_id } into gj
                                          from submember in gj.DefaultIfEmpty()
                                          where follow.followed_id == userId || submember == null
                                          join userDetail in _context.user_detail on follow.follower_id equals userDetail.UserId
                                          where userDetail.UserId != userId && follow.follower_id != userId
                                          select userDetail ;
                    var response = friendsToInvite.ToList();
                    return CustomResponse<object>.Success(response, true);
                }
                else if(where == 2)
                {
                    var friendsToInvite = from follow in _context.social_media_follows
                                          join member in _context.club_member
                                          on new { FriendId = follow.follower_id, ClubId = whereId } equals new { FriendId = member.user_id, ClubId = member.club_id } into gj
                                          from submember in gj.DefaultIfEmpty()
                                          where follow.followed_id == userId || submember == null
                                          join userDetail in _context.user_detail on follow.follower_id equals userDetail.UserId
                                          where userDetail.UserId != userId && follow.follower_id != userId
                                          select userDetail ;
                    var response = friendsToInvite.ToList();
                    return CustomResponse<object>.Success(response, true);
                }else if(where == 1)
                {
                    var friendsToInvite = from follow in _context.social_media_follows
                                          join userDetail in _context.user_detail on follow.follower_id equals userDetail.UserId
                                          where userDetail.UserId != userId && follow.follower_id != userId
                                          select userDetail;
                    var response = friendsToInvite.ToList();
                    return CustomResponse<object>.Success(response, true);
                }
                else
                {
                    return CustomResponse<object>.Success(null, true);
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

        public CustomResponse<object> InviteGroup(int senderId, string receiveIds, int groupId)
        {


            var group = _context.groups.Where(b => b.id == groupId).FirstOrDefault();
            int error = 0;
            List<string> list = new List<string>();
            foreach (var Id in receiveIds.Split(","))
            {
                try
                {
                    var newInvite = new UserInvites
                    {
                        inveting_id = senderId,
                        invited_id = Convert.ToInt32(Id),
                        where = 0,
                        whereId = groupId,
                        status = false,
                        invitedDateTime = DateTime.UtcNow
                    };
                    _context.user_invites.Add(newInvite);
                    _context.SaveChanges();
                    var invitedId = _context.user_invites.FirstOrDefault().invited_id;
                    _hubContext.Clients.User(Id).SendAsync("ReceiveGroupInviteNotification", group, invitedId);
                }
                catch (Exception ex)
                {
                    error++;
                    _logger.LogError("Error occured when user with " + senderId + " id is trying to invite user with " + groupId + " id to group with " + Id + " id. Error is " + ex.Message + " " + ex.InnerException);
                }
            }
            if (error == 0)
            {
                return CustomResponse<object>.Success(true);
            }
            else
            {
                return CustomResponse<object>.Success(false);
            }
        }

        public CustomResponse<object> InviteClub(int senderId, string receiveIds, int clubId)
        {
            var club = _context.clubs.Where(b => b.Id == clubId).FirstOrDefault();
            var invitedId = _context.user_invites.FirstOrDefault().invited_id;
            int error = 0;
            foreach (var Id in receiveIds.Split(","))
            {
                try
                {
                    var newInvite = new UserInvites
                    {
                        inveting_id = senderId,
                        invited_id = Convert.ToInt32(Id),
                        where = 2,
                        whereId = clubId,
                        status = false,
                        invitedDateTime = DateTime.UtcNow
                    };
                    _context.user_invites.Add(newInvite);
                    _hubContext.Clients.User(Id).SendAsync("ReceiveGroupInviteNotification", club, invitedId);
                }
                catch (Exception ex)
                {
                    error++;
                    _logger.LogError("Error occured when user with " + senderId + " id is trying to invite user with " + clubId + " id to club with " + Id + " id. Error is " + ex.Message + " " + ex.InnerException);
                }
                _context.SaveChanges();
            }
            if (error == 0)
            {
                return CustomResponse<object>.Success(true);
            }
            else
            {
                return CustomResponse<object>.Success(false);
            }



        }

        public CustomResponse<bool> InviteReply(int inviteId, bool inviteRep)
        {
            var invite = _context.user_invites.Where(b => b.Id == inviteId).FirstOrDefault();

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
