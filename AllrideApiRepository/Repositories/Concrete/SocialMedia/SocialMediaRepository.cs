using AllrideApiCore.Entities.SocialMedia;
using AllrideApiRepository.Repositories.Abstract.SocailMedia;

namespace AllrideApiRepository.Repositories.Concrete.SocialMedia
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly AllrideApiDbContext _context;
        public SocialMediaRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public List<SocialMediaFollow> GetUserFriends(SocialMediaFollow socialMediaFollow)
        {
            return _context.social_media_follows.Where(x=>x.followed_id == socialMediaFollow.followed_id).ToList();
        }
    }
}
