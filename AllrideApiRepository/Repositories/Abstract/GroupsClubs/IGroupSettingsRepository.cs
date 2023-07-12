using AllrideApiCore.Entities.Groups;

namespace AllrideApiRepository.Repositories.Abstract.GroupsClubs
{
    public interface IGroupSettingsRepository
    {
        public void Update();
        public void SaveChanges();
        public Group GetGroupById(int groupId);
        public int GetUserGroupRole(int groupId, int userId);
        public List<GroupMember> GetGroupMember(int groupId, List<int> memberIdList);
        public List<GroupMember> GetNewGroupMember(int groupId);

    }
}
