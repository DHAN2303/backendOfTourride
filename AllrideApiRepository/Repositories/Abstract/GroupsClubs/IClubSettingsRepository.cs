using AllrideApiCore.Entities.Clubs;

namespace AllrideApiRepository.Repositories.Abstract.GroupsClubs
{
    public interface IClubSettingsRepository
    {
        public void Update();
        public void SaveChanges();
        public Club GetClubById(int clubId);
        public int GetUserClubRole(int clubId, int userId);
        public List<ClubMember> GetClubMember(int clubId, List<int> memberIdList);
        public List<ClubMember> GetNewClubMember(int clubId);
    }
}
