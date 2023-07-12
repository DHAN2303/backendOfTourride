using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Groups
{
    public class GroupMember
    {
        public int id { get; set; }
        public int group_id { get; set; }
        public int user_id { get; set; }
        public int role { get; set; } // 0 creator, 1 admin, 2 kullanıcı
        public DateTime joined_date{ get; set; }
        public int active { get; set; }
        public IEnumerable<GroupSocialPost> GroupSocialPost { get; set; }

        [ForeignKey("group_id")]
        public Group Group { get; set; }

    }
}
