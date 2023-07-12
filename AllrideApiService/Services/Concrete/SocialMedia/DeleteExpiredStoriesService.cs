using AllrideApiRepository;
using AllrideApiService.Services.Abstract.SocialMedia;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class DeleteExpiredStoriesService : IDeleteExpiredStoriesService
    {
        protected readonly AllrideApiDbContext _context;
        public DeleteExpiredStoriesService(AllrideApiDbContext context)
        {
            _context = context;
        }


        public async Task DeleteExpiredStories()
        {

            var expiredStories = await _context.social_media_story
                .Where(s => s.created_at < DateTime.Now.AddHours(-24))
                .ToListAsync();

            _context.RemoveRange(expiredStories);
            await _context.SaveChangesAsync();
        }
    }
}
