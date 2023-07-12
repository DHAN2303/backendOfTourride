using AllrideApiCore.Entities.Groups;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class GroupSettingsRepository : IGroupSettingsRepository
    {
        protected AllrideApiDbContext _context;
        public GroupSettingsRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public Group GetGroupById(int groupId)
        {
            return _context.groups.FirstOrDefault(p => p.id == groupId);
        }
        public void Update()
        {
            _context.SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int GetUserGroupRole(int groupId, int userId)
        {
            return _context.group_member.Where(x => x.group_id == groupId).Where(x => x.user_id == userId).Select(x => x.role).FirstOrDefault();

        }
        public List<GroupMember> GetGroupMember(int groupId, List<int> memberIdList)
        {
            List<GroupMember> groupMember = new();
            foreach (int memberId in memberIdList)
            {
                groupMember = _context.group_member
                    .Where(g=>g.group_id == groupId)
                    .Where(gm => gm.user_id == memberId && gm.active == 1)
                    .ToList();
                
            }
            return groupMember;
        }
        public List<GroupMember> GetNewGroupMember(int groupId)
        {
            var groupMember = _context.group_member
                .Where(g => g.group_id == groupId)
                .Where(gm=>gm.active == 1)
                .ToList();
            return groupMember;
        }
    }
}
