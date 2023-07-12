using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiCore.Entities.SocialMedia;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.SocialMedia;

namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class SocialMediaCommentService : ISocialMediaCommentService
    {
        protected readonly AllrideApiDbContext _context;
        public SocialMediaCommentService(AllrideApiDbContext context)
        {
            _context = context;
        }

        public string SendCommentToPost(SocialMediaCommentsDto socialMediaComments)
        {
            var user_id = socialMediaComments.user_id;
            var post_id = socialMediaComments.post_id;
            var text = socialMediaComments.text;
            var post = _context.social_media_posts.FirstOrDefault(u => u.id == post_id);
            if (post != null)
            {
                var newComment = new SocialMediaComments
                {
                    user_id = user_id,
                    post_id = post_id,
                    text = text,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                };

                try
                {
                    post.comments_count++;
                    _context.social_media_comments.Add(newComment);
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when send comment: " + ex.InnerException);
                    return ex.InnerException.Message;
                }
            }
            else
            {
                return "false";
            }
        }

        public string EditComment(SocialMediaEditCommensDto socialMediaEdit)
        {
            var comment_id = socialMediaEdit.comment_id;
            var text = socialMediaEdit.text;
            var comment = _context.social_media_comments.FirstOrDefault(u => u.id == comment_id);
            if (comment != null)
            {
                try
                {
                    comment.text = text;
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when send comment: " + ex.InnerException);
                    return ex.InnerException.Message;
                }
            }
            else
            {
                return "false";
            }
        }



        public string DeleteComment(SocialMediaDeleteCommentsDto socialMediaDelete)
        {
            var comment_id = socialMediaDelete.comment_id;
            var post_id = socialMediaDelete.post_id;
            var comment = _context.social_media_comments.FirstOrDefault(u => u.id == comment_id);
            var post = _context.social_media_posts.FirstOrDefault(u => u.id == post_id);
            if (post != null && comment != null)
            {
                try
                {
                    post.comments_count--;
                    _context.Remove(comment);
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when send comment: " + ex.InnerException);
                    return ex.InnerException.Message;
                }
            }
            else
            {
                return "false";
            }

        }
    }
}
