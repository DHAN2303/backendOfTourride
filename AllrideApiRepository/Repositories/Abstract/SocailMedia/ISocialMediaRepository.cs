using AllrideApiCore.Entities.SocialMedia;

namespace AllrideApiRepository.Repositories.Abstract.SocailMedia
{
    public interface ISocialMediaRepository
    {
        public List<SocialMediaFollow> GetUserFriends(SocialMediaFollow socialMediaFollow);
    }
}
