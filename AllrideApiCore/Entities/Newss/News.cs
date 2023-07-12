using AllrideApiCore.Entities.Newss;
using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; } = new UserEntity();
        public string Image { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public IEnumerable<UserNewsReaction> UserNewsReactions { get; set; } = new List<UserNewsReaction>();
        public NewsTags NewsTags { get; set; }
        
    }
}
