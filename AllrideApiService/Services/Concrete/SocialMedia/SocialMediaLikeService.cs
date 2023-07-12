using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiCore.Entities.SocialMedia;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.SocialMedia;
using AllrideApiService.Services.Concrete.UserCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class SocialMediaLikeService : ISocialMediaLikeService
    {
        protected AllrideApiDbContext _context;
        private readonly ILogger<LoginService> _logger;

        public SocialMediaLikeService(AllrideApiDbContext context, ILogger<LoginService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string PostLike([FromForm] SocialMediaLikeDto socialMediaLike, int userId)
        {
            var userid = userId;
            var postid = socialMediaLike.post_id;
            var post = _context.social_media_posts.FirstOrDefault(u => u.id == postid);

            var newLike = new SocialMediaLike()
            {
                user_id = userid,
                post_id = postid,
                created_at = DateTime.UtcNow,
            };

            if (post != null)
                try
                {
                    post.LikedByUsers = new List<int> { userId };
                    _context.social_media_likes.Add(newLike);
                    post.likes_count++;
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("when erroe post like: " + ex.InnerException.Message);
                    return ex.InnerException.Message;

                }
            else return "false";

        }

        public string PostUnLike([FromForm] SocialMediaUnLikeDto socialMediaUnLike, int userId)
        {
            var userid = userId;
            var postid = socialMediaUnLike.post_id;
            var post = _context.social_media_posts.FirstOrDefault(u => u.id == postid);
            var like = _context.social_media_likes.FirstOrDefault(u => u.user_id == userid && u.post_id == postid);

            if (like != null && post != null)
                try
                {
                    post.likes_count--;
                    post.LikedByUsers.Remove(userid);
                    _context.Remove(like);
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError("when erroe post like: " + ex.InnerException.Message);
                    return ex.InnerException.Message;
                }
            else return "false";
        }
    }
}
