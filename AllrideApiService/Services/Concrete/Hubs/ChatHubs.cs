using AllrideApiChat.DataBase;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Concrete.Hubs.Data;
using AllrideApiService.Services.Concrete.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace AllrideApi.Hubs
{
    public class ChatHubs:Hub
    {
        private readonly AllrideApiDbContext _context;
        private IPosts _posts;

        public ChatHubs(AllrideApiDbContext context, IPosts posts)
        {
            _context = context;
            _posts = posts;
        }

        public async Task SendPeerToPeerMessage(int senderid, int receiveid, int content_type, string message)
        {
            try
            {
                var connectionId = ClientSource.Clients.FirstOrDefault(b => b.NickName == receiveid.ToString());
                if(connectionId != null)
                await Clients.Client(connectionId.ConnectionId).SendAsync("ReceivePersonMessage", senderid, receiveid, content_type, message);
                 await _posts.PostMessageAsync(senderid, receiveid, content_type, message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + " " + e.InnerException);
            }

        }

        public async Task SendPersonFiles(int senderid, int receiveid,int content_type ,string path)
        {
            var connectionId = ClientSource.Clients.Where(b => b.NickName == receiveid.ToString()).First().ConnectionId;
            await Clients.Client(connectionId).SendAsync("ReceivePersonFiles", senderid, receiveid, content_type, path);
            await _posts.PostMessageAsync(senderid, receiveid, content_type, path);
        }

        //group
        public async Task SendMessageToGroup(int senderid,int groupId, int content_type,string message)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveGroupMessage",  senderid, groupId,content_type, message);
            await _posts.PostGroupMessageAsync(groupId,senderid,0, message);
        }

        public async Task SendGroupFiles(int senderid, int groupId, int content_type, string path)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveGroupFiles", senderid, groupId, content_type, path);
            await _posts.PostGroupMessageAsync(groupId, senderid, content_type, path);
        }

        public async Task JoinGroup()
        {
            var userId = ClientSource.Clients.Where(b => b.ConnectionId == Context.ConnectionId).First().NickName;
            var userGroups = _context.group_member.Where(b => b.user_id == Convert.ToInt32(userId)).ToList();
            foreach (var group in userGroups)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group.group_id.ToString());
            }
        }

        public async Task LeaveGroup()
        {
            var userId = ClientSource.Clients.Where(b => b.ConnectionId == Context.ConnectionId).First().NickName;
            var userGroups = _context.group_member.Where(b => b.user_id == Convert.ToInt32(userId)).ToList();
            foreach (var group in userGroups)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.group_id.ToString());
            }
        }
        //


        //CLUB
        public async Task SendMessageToClub(int senderid, int clubId, int content_type, string message)
        {
            await Clients.Group(clubId.ToString()).SendAsync("ReceiveClubMessage", senderid, clubId, content_type, message);
            await _posts.PostClubMessageAsync(clubId, senderid, content_type, message);
        }

        public async Task SendClubFiles(int senderid, int clubId, int content_type, string path)
        {
            await Clients.Group(clubId.ToString()).SendAsync("ReceiveClubFiles", senderid, clubId, content_type, path);
            await _posts.PostGroupMessageAsync(clubId, senderid, content_type, path);
        }

        public async Task JoinClub()
        {
            var userId = ClientSource.Clients.Where(b => b.ConnectionId == Context.ConnectionId).First().NickName;
            var userClubs = _context.club_member.Where(b => b.user_id == Convert.ToInt32(userId)).ToList();
            foreach (var club in userClubs)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, club.club_id.ToString());
            }
        }

        public async Task LeaveClub()
        {
            var userId = ClientSource.Clients.Where(b => b.ConnectionId == Context.ConnectionId).First().NickName;
            var userClubs = _context.club_member.Where(b => b.user_id == Convert.ToInt32(userId)).ToList();
            foreach (var club in userClubs)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, club.club_id.ToString());
            }
        }


        //location
        public async Task UpdatePersonLocation(string userId, double latitude, double longitude)
        {
            var connectionId = ClientSource.Clients.Where(b => b.NickName == userId).First().ConnectionId;
            await Clients.User(connectionId).SendAsync("ReceivePersonLocation", userId, latitude, longitude);
        }

        public async Task UpdateGroupLocation(string groupId, double latitude, double longitude)
        {
            await Clients.Group(groupId).SendAsync("ReceiveGroupLocation", groupId, latitude, longitude);
        }

        public async Task UpdateClubLocation(string clubId, double latitude, double longitude)
        {
            await Clients.Group(clubId).SendAsync("ReceiveClubLocation", clubId, latitude, longitude);
        }


        //notification
        public async Task SendPersonScheduledNotifications()
        {
            var currentDate = DateTime.UtcNow;

            var notifications = _context.person_notification
                .Where(n => n.sendDateTime <= currentDate)
                .ToList();

            foreach (var notification in notifications)
            {
                await Clients.Client(notification.userId.ToString()).SendAsync("ReceivePersonNotification", notification);
            }
        }

        public async Task SendGroupScheduledNotifications()
        {
            var currentDate = DateTime.UtcNow;

            var notifications = _context.group_notification
                .Where(n => n.sendDateTime <= currentDate)
                .ToList();

            foreach (var notification in notifications)
            {
                await Clients.Group(notification.groupId.ToString()).SendAsync("ReceiveGroupNotification", notification);
            }
        }

        public async Task SendClubScheduledNotifications()
        {
            var currentDate = DateTime.UtcNow;

            var notifications = _context.club_notification
                .Where(n => n.sendDateTime <= currentDate)
                .ToList();

            foreach (var notification in notifications)
            {
                await Clients.Group(notification.clubId.ToString()).SendAsync("ReceiveGroupNotification", notification);
            }
        }

        public async Task SendInviteNotification(int userId, string message)
        {
            await Clients.Client(userId.ToString()).SendAsync("ReceiveInviteNotification", message);
        }


        //status
        public async Task OnConnectedAsync(string userId)
        {
            await UpdateUserStatus(userId, true);
            await base.OnConnectedAsync();
            Console.WriteLine(userId + "is online");
        }

        private Task UpdateUserStatus(string userId, bool isOnline)
        {
            if (isOnline)
            {
                ClientModel client = new ClientModel
                {
                    ConnectionId = Context.ConnectionId,
                    NickName = userId,
                };
                ClientSource.Clients.Add(client);
                var newOnline = new OnlineUsers
                {
                    userId = Convert.ToInt32(userId)
                };
                _context.online_users.Add(newOnline);
                _context.SaveChanges();
            }
            else
            {
                ClientModel client = ClientSource.Clients.Where(b => b.ConnectionId == Context.ConnectionId).First();
                ClientSource.Clients.Remove(client);

                var data = _context.online_users.Where(d => d.userId == Convert.ToInt32(userId)).FirstOrDefault();
                _context.online_users.Remove(data);
            }
            return Task.CompletedTask;
        }

        public async Task OnDisconnectedAsync(Exception exception, string userId)
        {
            await UpdateUserStatus(userId, false);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task TypingStatus(string clientId, bool status)
        {
            var connectionId = ClientSource.Clients.FirstOrDefault(b => b.NickName == clientId);
            await Clients.Client(connectionId.ConnectionId).SendAsync("ReceiveTypingStatus", status);
        }

        public async Task LikeStatus(string clientId)
        {
            var connectionId = ClientSource.Clients.FirstOrDefault(b => b.NickName == clientId);
            if (connectionId != null)
            {
                var query = from post in _context.social_media_posts
                            where post.user_id == Convert.ToInt32(clientId) || _context.social_media_follows.Any(follow => follow.followed_id == post.user_id && follow.follower_id == Convert.ToInt32(clientId))
                            orderby post.created_at descending
                            select post;
                var postLikes = query.ToList();
                var data = CustomResponse<Object>.Success(postLikes, true);
                List<int> likedByUsersList = postLikes.SelectMany(item => item.LikedByUsers).ToList();
                string json = JsonSerializer.Serialize(postLikes, new JsonSerializerOptions
                {
                    WriteIndented = true // Set any additional serialization options here
                });

                await Clients.Client(connectionId.ConnectionId).SendAsync("ReceiveLikeStatus", data);
            }

        }
    }
}
