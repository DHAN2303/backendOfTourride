namespace AllrideApiCore.Entities.Clubs
{
    public class ClubSocialPostComment:BaseEntity
    {
        public int ClubMemberId { get; set; }
        public int ClubSocialPostId { get; set; }
        public string Comment { get; set; }
        public ClubMember ClubMember { get; set; }
        public ClubSocialPost ClubSocialPost { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
