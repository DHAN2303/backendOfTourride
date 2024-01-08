using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Clubs;
using AllrideApiRepository.Repositories.Abstract.Clubs;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class ClubRepository: IClubRepository
    {
        private readonly AllrideApiDbContext _context;
        private readonly ILogger<ClubRepository> _logger;
        public ClubRepository(AllrideApiDbContext context, ILogger<ClubRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool DeleteClub(int ClubId)
        {
            try
            {
                var expiredStories = _context.clubs.FirstOrDefault(e => e.Id == ClubId);
                _context.Remove(expiredStories);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteUserInClub(int ClubId, int UserId)
        {
            try
            {
                var expiredStories = _context.club_member.FirstOrDefault(e => e.club_id == ClubId && e.user_id == UserId);
                _context.Remove(expiredStories);
                checkGroupAdmin(ClubId);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public ClubResponseDto GetClubDetail(int ClubId)
        {
            // return _context.club.FirstOrDefault(x => x.Id == ClubId);

            var result = (
                from c in _context.clubs
                join cm in _context.club_member on c.Id equals cm.club_id
                join ud in _context.user_detail on cm.user_id equals ud.Id
                where c.Id == ClubId
                select new ClubResponseDto
                {
                    name = c.name,
                    image_path = c.image_path,
                    description = c.description,
                    created_date = c.created_date,
                   // club_rank = c.club_rank,
                    club_admin = ud.Name
                }
            ).FirstOrDefault();

            return result;

        }

        public  List<UserResponseDto> GetClubUserDetail(int ClubId)
        {
            var item = (
            from g in _context.club_member
            join u in _context.user_detail on g.user_id equals u.Id
            where (g.club_id == ClubId)
            select new UserResponseDto
            {
                Name = u.Name,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender,
                Phone = u.Phone,
                Country = u.Country,
                Language = u.Language,
                PpPath = u.PpPath
            }).ToList();
            return item;
        }

        public List<GlobalClubResponseDto> GetGlobalClubs(int ClubId, int Type)
        {
            using (_context)
            {
                return _context.clubs
                    .Where(e => e.Id == ClubId && e.type == Type)
                    .Select(e => new GlobalClubResponseDto
                    {
                        id = e.Id,
                        name = e.name,
                    })
                    .ToList();
            }
        }

        public Club GetMedia(int clubId)
        {
            return _context.clubs.FirstOrDefault(x => x.Id == clubId);
        }

        public bool IsExistClub(int ClubId)
        {
            return _context.clubs.Any(e => e.Id == ClubId);
        }

        public bool IsExistUserInClub(int ClubId, int UserId)
        {
            return _context.club_member.Any(e => e.club_id == ClubId && e.user_id == UserId);
        }

        public List<Club> SearchClub(string clubName)
        {
            return _context.clubs.Where(x=>x.Equals(clubName)).ToList();
        }

        public List<string> SearchUserClub(string UserName, int ClubId)
        {
            var userNameList = (
              from c in _context.club_member
              join u in _context.user_detail on c.user_id equals u.Id
              where (c.club_id == ClubId)
              select u.Name
              ).ToList();
            return userNameList;
        }

        public List<Club> GetClubsForUser(int userId)
        {
            var clubs = _context.club_member
                .Where(gm => gm.user_id == userId)
                .Join(_context.clubs,
                    gm => gm.club_id,
                    g => g.Id,
                    (gm, g) => g)
                .ToList();
            return clubs;
        }


        public List<ClubMessage> GetClubMessage(int clubId)
        {
            return _context.club_messages.Where(m => m.club_id == clubId).ToList();
        }


        public void checkGroupAdmin(int clubId)
        {
            var members = _context.club_member.Where(member => member.club_id == clubId).ToList();

            if (members.Count > 0)
            {
                var response = _context.club_member.Any(member =>
                member.club_id == clubId && member.role == 0);

                if (!response)
                {
                    members[0].role = 0;
                    _context.SaveChanges();
                }

            }
            else
            {
                var club = _context.clubs.FirstOrDefault(b => b.Id == clubId);
                _context.clubs.Remove(club);
                _context.SaveChanges();
            }
        }
    }
}
