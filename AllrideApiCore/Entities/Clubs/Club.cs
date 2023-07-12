using AllrideApiCore.Entities.Activities;
using AllrideApiCore.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Clubs
{
#nullable enable
    public class Club : BaseEntity
    {
        public string name { get; set; }
        public int CreatorId { get; set; }
        public string backgroundCover_path { get; set; }
        public string profile_path { get; set; }
        public string image_path { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public int is_invite { get; set; }
        public List<string>? group_members { get; set; }
        public IEnumerable<Activity> activities { get; set; }
        public IEnumerable<ClubSocialPost> ClubSocialPost { get; set; }
        //public IEnumerable<ClubMember> ClubMembers { get; set; }
        //public IEnumerable<RoutePlanner> RoutePlanners { get; set; }

        [ForeignKey("CreatorId")]
        public UserEntity User { get; set; }


    }
}
