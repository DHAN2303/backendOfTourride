using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.SocialMedia;
using AllrideApiService.Services.Concrete.UserCommon;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class SocialMediaPostService : ISocialMediaPostsService
    {
        protected AllrideApiDbContext _context;
        private readonly ILogger<LoginService> _logger;

        public SocialMediaPostService(AllrideApiDbContext context, ILogger<LoginService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public bool UpdatePost(SocialMediaUpdatePostDto socialMediaUpdate)
        {
            var post_id = socialMediaUpdate.post_id;
            var caption = socialMediaUpdate.caption;
            var location = socialMediaUpdate.location;

            var post = _context.social_media_posts.FirstOrDefault(u => u.id == post_id);

            if (post != null)
                try
                {
                    if (caption != null) post.caption = caption;
                    if (location != null) post.location_info = location;
                    post.updated_at = DateTime.UtcNow;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Update post error: " + ex.InnerException);
                    return false;
                }
            else return false;
        }


        public bool DeletePost(SocialMediaDeletePostDto socialMediaDelete)
        {
            var post_id = socialMediaDelete.post_id;
            var post = _context.social_media_posts.FirstOrDefault(u => u.id == post_id);

            if (post != null)
                try
                {
                    _context.Remove(post);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Delete post error: " + ex.InnerException);
                    return false;
                }
            else return true;
        }

        public object FetchPost(int userId)
        {

            var query = from post in _context.social_media_posts
                        where post.user_id == userId || _context.social_media_follows.Any(follow => follow.followed_id == post.user_id && follow.follower_id == userId)
                        orderby post.created_at descending
                        select post;

            var posts = query.ToList();
            if (posts.Count > 0)
                return posts;
            else
                return null;
        }
    }
}
