using AllrideApiCore.Entities.Clubs;
using AllrideApiCore.Entities.Groups;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities.Activities
{
    public class Activity:BaseEntity
    {
        public string name { get; set; }
        public int creator_user_id { get; set; }
        public int group_id { get; set; }
        public int club_id { get; set; }
        public int route_id { get; set; }
        public DateTime created_date { get; set; } = DateTime.Now;
        public DateTime start_date { get; set; }
        public Route Route { get; set; }
        public Group Group { get; set; }
        public Club  Club { get; set; }
        public UserEntity Users { get; set; }
        public IEnumerable<ActivityMember> ActivityMembers { get; set; } 
    }
}
