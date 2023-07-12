using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities.Clubs
{
    public class ClubMember
    {
        public int id { get; set; }
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public int club_id { get; set; }
        public int user_id { get; set; }
        public int role { get; set; }
        public int active { get; set; }
        public DateTime joined_date { get; set; }
        public IEnumerable<ClubSocialPost> ClubSocialPost { get; set; }

        public Club Club { get; set; }
        public UserEntity User { get; set; }
    }
}
