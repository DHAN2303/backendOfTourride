using AllrideApiCore.Entities.Chat;

namespace AllrideApiCore.Entities.Groups
{
    public class GroupSocialPost:BaseEntity
    {
        public int GroupId { get; set; }
        public int GroupMemberId { get; set; }
        public string[] PostImagePath { get; set; }
        public string HashTag { get; set; }
        public string Description { get; set; }
        public int LikeUnlikeCount { get; set; }
        public Group Group { get; set; }
        public GroupMember GroupMember { get; set; }
        public List<GroupSocialPostComment> GroupSocialPostComment { get; set; }
        public DateTime DeletedDate { get; set; }

    }
}
