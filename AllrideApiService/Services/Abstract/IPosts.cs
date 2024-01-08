using AllrideApiCore.Dtos.RequestDto;
using Microsoft.AspNetCore.Http;

namespace AllrideApiService.Services.Abstract
{
    public interface IPosts
    {
        public Task PostLogAsync(string _client_ip, string _service_name, string _request_param, int _response_status, string _responseText, int _api_type);
        public Task PostGroupMessageAsync(int groupId, int senderId, int content_type, string message_content);
        public Task PostMessageAsync(int senderId, int recipientId, int content_type, string message_content);
        public Task PostSocialMediaPostsAsync(int userId, string caption, string location, string mediaUrl);
        public Task PostNewGroup(string groupName, IFormFile groupImage, string description, List<string> groupMember, int admin);
        public Task CreateNewClub(ClubRequestDto clubRequestDto);
        public string PostSocialMediaStory(int userId, string caption, string location, string mediaUrl);
        public Task PostClubMessageAsync(int clubId, int senderId, int content_type, string message_content);


    }
}
