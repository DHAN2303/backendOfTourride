using AllrideApiCore.Entities.Activities;
using AllrideApiCore.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Groups
{
    public class Group
    {
        public int id { get; set; }
        public int CreatorId { get; set; }
        public string name { get; set; }    
        public string image_path { get; set; }
        public string backgroundCover_path { get; set; }  // DB DE SÜTUN AÇILACAK
        public string description { get; set; }
        public int group_rank { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set;}
        public int type { get; set; }
        public int is_invite { get; set; }
        public IEnumerable<Activity> activities { get; set; }
        public IEnumerable<GroupSocialPost> GroupSocialPost { get; set; }

        [ForeignKey("CreatorId")]
        public UserEntity User { get; set; }
    }
}
