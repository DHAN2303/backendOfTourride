using AllrideApiCore.Entities.Clubs;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class ClubSettingsRepository : IClubSettingsRepository
    {
        protected AllrideApiDbContext _context;
        public ClubSettingsRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public Club GetClubById(int clubId)
        {
            return _context.club.FirstOrDefault(p => p.Id == clubId);
        }
        public void Update()
        {
            _context.SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public int GetUserClubRole(int clubId, int userId)
        {
            return _context.club_member.Where(x => x.ClubId == clubId).Where(x => x.UserId == userId).Select(x => x.role).FirstOrDefault();
        }

        public List<ClubMember> GetClubMember(int clubId, List<int> memberIdList)
        {
            List<ClubMember> clubMember = new();
            foreach (int memberId in memberIdList)
            {
                clubMember = _context.club_member
                    .Where(c => c.ClubId == clubId)
                    .Where(cm => cm.UserId == memberId && cm.active == 1)
                    .ToList();

            }
            return clubMember;
        }

        public List<ClubMember> GetNewClubMember(int clubId)
        {

            var clubMember = _context.club_member
                .Where(g => g.ClubId == clubId)
                .Where(gm => gm.active == 1)
                .ToList();
            return clubMember;
        }


    }
}
