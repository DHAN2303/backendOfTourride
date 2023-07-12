using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Clubs;
using AllrideApiRepository.Repositories.Abstract.Clubs;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class ClubRepository : IClubRepository
    {
        protected readonly AllrideApiDbContext _context;
        private readonly ILogger<ClubRepository> _logger;
        public ClubRepository(AllrideApiDbContext context, ILogger<ClubRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // 13 Haziran
        public List<UserProfileResponseDto> GetClubsUsers(int ClubId)
        {
            //return _context.group_member.Where(x=>x.group_id == GroupId).Select(x=>x.user_id).FirstOrDefault();

            var userList = (
                  from c in _context.club_member
                  join u in _context.user_detail on c.user_id equals u.UserId
                  where c.id == ClubId
                  select new UserProfileResponseDto
                  {
                      Name = u.Name,
                      LastName = u.LastName,
                      PpPath = u.PpPath
                  }
                  ).ToList();
            return userList;
        }
        public bool DeleteClub(int ClubId)
        {
            try
            {
                var expiredStories = _context.club.FirstOrDefault(e => e.Id == ClubId);
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
                var expiredStories = _context.club_member.
                    FirstOrDefault(e => e.club_id == ClubId && e.user_id == UserId);
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


        public UserResponseDto GetClubUserDetail(int ClubId)
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
            }).FirstOrDefault();
            return item;
        }

        public List<GlobalClubResponseDto> GetGlobalClubs(int ClubId, int Type)
        {
            using (_context)
            {
                return _context.club
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
            return _context.club.FirstOrDefault(x => x.Id == clubId);
        }

        public bool IsExistClub(int ClubId)
        {
            return _context.club.Any(e => e.Id == ClubId);
        }

        public bool IsExistUserInClub(int ClubId, int UserId)
        {
            return _context.club_member.Any(e => e.club_id == ClubId && e.user_id == UserId);
        }

        public List<Club> SearchClub(string clubName)
        {
            /* var userNameList = (
              from c in _context.club_member
              join u in _context.user_detail on c.UserId equals u.Id
              where (c.ClubId == ClubId)
              join u in _context.user_detail on c.user_id equals u.Id
              where (c.club_id == ClubId)
              select u.Name
              ).ToList();
             return userNameList;
            */
            return _context.club.Where(x => x.Equals(clubName)).ToList();
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
        public ClubResponseDto GetClubDetail(int ClubId)
        {
            // return _context.club.FirstOrDefault(x => x.Id == ClubId);

            var result = (
                from c in _context.club
                join cm in _context.club_member on c.Id equals cm.ClubId
                join ud in _context.user_detail on cm.UserId equals ud.Id
                where c.Id == ClubId
                select new ClubResponseDto
                {
                    name = c.name,
                    profile_path = c.profile_path,
                    description = c.description,
                    created_date = c.CreatedDate,
                    // club_rank = c.club_rank,
                    club_admin = ud.Name
                }
            ).FirstOrDefault();

            return result;

        }
        public LastActivityResponseDto GetLastActivity(int ClubId)
        {
            var lastAct = (
              from a in _context.activity
              join c in _context.club on a.club_id equals c.Id
              join r in _context.route on a.route_id equals r.Id
              join u in _context.user_detail on a.creator_user_id equals u.Id
              where (a.club_id == ClubId)
              where (a.created_date == r.CreatedDate)
              orderby a.created_date descending
              select new LastActivityResponseDto
              {
                  ActivityName = a.name,
                  Duration = r.Duration,
                  Distance = r.Length,
                  CreatedDate = a.created_date,
                  StartedDate = a.start_date,
                  Geoloc = r.Geoloc,
                  CreaterName = u.Name,
                  NameLastName = u.Name,
                  UserProfileImagePath = u.PpPath
              }).FirstOrDefault();
            return lastAct;
        }
        public List<ClubResponseDto> GetUsersClubList(int userId)
        {
            var getUserClubList = (
                   from c in _context.club
                   join cm in _context.club_member on c.Id equals cm.ClubId
                   join u in _context.user on cm.UserId equals u.Id
                   join ud in _context.user_detail on u.Id equals ud.UserId
                   where cm.UserId == userId && cm.role == 0
                   group new { c, ud } by c into clb
                   select new ClubResponseDto
                   {
                       name = clb.Key.name,
                       backgroundCover_path = clb.Key.backgroundCover_path,
                       description = clb.Key.description,
                       club_admin = clb.Select(x => x.ud.Name).FirstOrDefault(),  // Kullanıcı adını al
                       created_date = clb.Key.CreatedDate,
                       member_count = clb.Count()  // Grup üye sayısını al
                   }
                   ).ToList();
            return getUserClubList;
        }
        public List<ClubSocialMediaPostsResponseDto> GetClubUsersSocialMediaLast3Post(int clubId)
        {
            var result = (
                from cm in _context.club_member
                join c in _context.club on cm.ClubId equals c.Id
                join u in _context.user on cm.UserId equals u.Id
                join ud in _context.user_detail on u.Id equals ud.UserId
                join csp in _context.clubsocial_post on cm.id equals csp.ClubMemberId
                join cspc in _context.clubsocial_postcomment on csp.Id equals cspc.ClubSocialPostId
                where (cm.ClubId == clubId)
                where (cm.UserId == u.Id && cm.UserId == ud.UserId)
                where (cspc.ClubMemberId == cm.UserId && csp.ClubMemberId == cm.UserId)
                orderby csp.CreatedDate descending
                select new ClubSocialMediaPostsResponseDto
                {
                    UserName = ud.Name,
                    Description = csp.Description,
                    HashTag = csp.HashTag,
                    ImagePath = ud.PpPath,

                    ClubSocialPostCommentDtos = new List<ClubSocialPostCommentDto>
                    {
                        new ClubSocialPostCommentDto
                        {
                            UserName = ud.Name,
                            UserPp = ud.PpPath,
                            Comment =cspc.Comment
                        }
                    }
                })
                .Take(6)
                .ToList();
            return result;
        }
        public int GetClubMemberCount(int ClubId)
        {
            var result = _context.club_member.Where(x => x.ClubId == ClubId).Where(x => x.role == 1).Count();
            return result;

        }
    }
}
