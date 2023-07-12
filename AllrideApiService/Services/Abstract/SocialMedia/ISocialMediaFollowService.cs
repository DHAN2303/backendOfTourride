using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface ISocialMediaFollowService
    {
        public string AddFollower(SocialMediaFollowDto socialMediaFollow);
        public string UnFollower(SocialMediaUnFollowDto socialMediaUnFollow);
        public CustomResponse<SocialMediaFollowDto> GetUserFriends(SocialMediaFollowDto socialMediaFollow);

    }
}
