using AllrideApiCore.Entities.Chat;

namespace AllrideApiCore.Entities.Groups
{
    public class GroupSocialPostComment:BaseEntity
    {   
        public int GroupMemberId { get; set; }
        public int GroupSocialPostId { get; set; }
        public string Comment { get; set; }
        public GroupMember GroupMember { get; set; }
        public GroupSocialPost GroupSocialPost { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
