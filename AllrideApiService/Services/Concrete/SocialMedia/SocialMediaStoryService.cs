using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.SocialMedia;


namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class SocialMediaStoryService : ISocialMediaStoryService
    {
        protected AllrideApiDbContext _context;
        public SocialMediaStoryService(AllrideApiDbContext context)
        {
            _context = context;
        }


        public string UpdatePost(SocialMediaUpdateStoryDto socialMediaUpdateStory)
        {
            var story_id = socialMediaUpdateStory.story_id;
            var caption = socialMediaUpdateStory.caption;
            var location = socialMediaUpdateStory.location;

            var story = _context.social_media_story.FirstOrDefault(u => u.id == story_id);

            if (story != null)
                try
                {
                    if (caption != null) story.caption = caption;
                    if (location != null) story.location_info = location;
                    story.updated_at = DateTime.UtcNow;
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Update post error: " + ex.InnerException);
                    return ex.Message;
                }
            else return "false";
        }

        public string DeletePost(SocialMediaDeleteStoryDto socialMediaDelete)
        {
            var story_id = socialMediaDelete.story_id;
            var story = _context.social_media_story.FirstOrDefault(u => u.id == story_id);

            if (story != null)
                try
                {
                    _context.Remove(story);
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Delete post error: " + ex.InnerException);
                    return ex.Message;
                }
            else return "false";
        }


        public object FetchStory(int userId)
        {
            var query = from follow in _context.social_media_follows
                        join story in _context.social_media_story
                        on follow.follower_id equals story.user_id
                        where follow.followed_id == userId
                        orderby story.created_at descending
                        select story;

            var stories = query.ToList();
            if (stories != null)
                return stories;
            else return null;
        }
    }
}
