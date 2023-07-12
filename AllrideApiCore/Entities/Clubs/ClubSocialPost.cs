using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Clubs
{
    public class ClubSocialPost:BaseEntity
    {
        public int ClubId { get; set; }
        public int ClubMemberId { get; set; }
        public string[] PostImagePath { get; set; }
        public string HashTag { get; set; }
        public string Description { get; set; }
        public int LikeUnlikeCount { get; set; }

        [ForeignKey("ClubId")]
        public Club Club { get; set; }
        public ClubMember ClubMember { get; set; }
        public List<ClubSocialPostComment> ClubSocialPostComment { get; set; }
        public DateTime DeletedDate { get; set; }

    }

}
