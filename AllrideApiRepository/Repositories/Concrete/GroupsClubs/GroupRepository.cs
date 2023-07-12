using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Groups;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class GroupRepository : IGroupRepository // <T> : IGroupClubBaseRepository<T>
    {
        protected readonly AllrideApiDbContext _context;
        private readonly ILogger<GroupRepository> _logger;
        public GroupRepository(AllrideApiDbContext context, ILogger<GroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // 13 Haziran
        public List<UserProfileResponseDto> GetGroupsUsers(int GroupId)
        {
            //return _context.group_member.Where(x=>x.group_id == GroupId).Select(x=>x.user_id).FirstOrDefault();

            var userList = (
                  from g in _context.group_member
                  join u in _context.user_detail on g.user_id equals u.UserId
                  where g.id == GroupId
                  select new UserProfileResponseDto
                  {
                      Name = u.Name,
                      LastName = u.LastName,
                      PpPath = u.PpPath
                  }
                  ).ToList();
            return userList;
        }

        public GroupMember  GetGroupMember(int GroupId)
        {
            return _context.group_member.SingleOrDefault(x=>x.group_id== GroupId);
        }

        public bool DeleteUserInGroup(int GroupId, int UserId)
        {
            try
            {
                var expiredStories = _context.group_member.FirstOrDefault(e => e.group_id == GroupId && e.user_id == UserId);
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

        public bool IsExistUserInGroup(int GroupId, int UserId)
        {
            return _context.group_member.Any(e => e.group_id == GroupId && e.user_id == UserId);
        }

        public bool DeleteGroup(int GroupId)
        {
            try
            {
                var expiredStories = _context.groups.FirstOrDefault(e => e.id == GroupId);
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

        public bool IsExistGroup(int GroupId)
        {
            return _context.groups.Any(e => e.id == GroupId);
        }


        public List<GlobalGroupResponseDto> GetGlobalGroups(int GroupId, int Type)
        {
            using (_context)
            {
                return _context.groups
                    .Where(e => e.id == GroupId && e.type == Type)
                    .Select(e => new GlobalGroupResponseDto
                    {
                        id = e.id,
                        name = e.name,
                    })
                    .ToList();
            }
        }

        public GroupResponseDto GetGroupDetail(int GroupId)
        {

            var result = (
                from g in _context.groups
                join gm in _context.group_member on g.id equals gm.group_id
                join ud in _context.user_detail on gm.user_id equals ud.Id
                where g.id == GroupId
                select new GroupResponseDto
                {
                    name = g.name,
                    backgroundCover_path = g.image_path,
                    image_path = g.image_path,
                    description = g.description,
                    created_date = g.created_date,
                    group_rank = g.group_rank,
                    group_admin = ud.Name
                }
            ).FirstOrDefault();

            return result;

        }


        public List<GroupResponseDto> GetMedia(int GroupId)
        {
            using (_context)
            {
                return _context.groups
                    .Where(e => e.id == GroupId)
                    .Select(e => new GroupResponseDto
                    {
                        name = e.name,
                        backgroundCover_path = e.image_path,
                        image_path = e.image_path,
                        description = e.description
                    }).ToList();
            }
        }

        public UserResponseDto GetGroupUserDetail(int GroupId)
        {
            var item = (
              from g in _context.group_member
              join u in _context.user_detail on g.user_id equals u.Id
              where (g.group_id == GroupId)
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

        public List<string> SearchUserGroup(string UserName, int GroupId)
        {
            var userNameList = (
              from g in _context.group_member
              join u in _context.user_detail on g.user_id equals u.Id
              where (g.group_id == GroupId)
              select u.Name
              ).ToList();
            return userNameList;

        }

        public List<Group> SearchGroup(string groupName)
        {
            return _context.groups.Where(x => x.Equals(groupName)).ToList();
        }
        public List<GroupResponseDto> GetUsersGroupList(int userId)
        {
            var getUserGroupList = (
                   from g in _context.groups
                   join gm in _context.group_member on g.id equals gm.group_id
                   join u in _context.user on gm.user_id equals u.Id
                   join ud in _context.user_detail on u.Id equals ud.UserId
                   where gm.user_id == userId && gm.role == 0
                   group new { g, ud } by g into grp
                   select new GroupResponseDto
                   {
                       name = grp.Key.name,
                       backgroundCover_path = grp.Key.backgroundCover_path,
                       description = grp.Key.description,
                       group_rank = grp.Key.group_rank,
                       group_admin = grp.Select(x => x.ud.Name).FirstOrDefault(),  // Kullanıcı adını al
                       created_date = grp.Key.created_date,
                       member_count = grp.Count()  // Grup üye sayısını al
                   }
                   ).ToList();
            return getUserGroupList;
        }
        public List<GroupSocialMediaPostsResponseDto> GetGroupsUsersSocialMediaLast3Post(int groupId)
        {
            var result = (
              from gm in _context.group_member
              join g in _context.groups on gm.group_id equals g.id
              join u in _context.user on gm.user_id equals u.Id
              join ud in _context.user_detail on u.Id equals ud.UserId
              join gsp in _context.groupsocial_post on gm.id equals gsp.GroupMemberId
              join gspc in _context.groupsocial_postcomment on gsp.Id equals gspc.GroupSocialPostId
              where (gm.group_id == groupId)
              where (gm.user_id == u.Id && gm.user_id == ud.UserId)
              where (gspc.GroupMemberId == gm.user_id && gsp.GroupMemberId == gm.user_id)
              orderby gsp.CreatedDate descending
              select new GroupSocialMediaPostsResponseDto
              {
                  UserName = ud.Name,
                  Description = gsp.Description,
                  HashTag = gsp.HashTag,
                  ImagePath = ud.PpPath,

                  GroupSocialPostCommentDtos = new List<GroupSocialPostCommentDto>
                  {
                        new GroupSocialPostCommentDto
                        {
                            UserName = ud.Name,
                            UserPp = ud.PpPath,
                            Comment = gspc.Comment
                        }
                  }
              })
              .Take(6)
              .ToList();
            return result;
        }

        public LastActivityResponseDto GetLastActivity(int GroupId)
        {
            throw new NotImplementedException();
        }

        public string UpdateProfileOrBacgroundImage(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}





//var results = _context.groups
//        .Join(_context.group_member,
//            t1 => t1.CommonColumn,
//            t2 => t2.CommonColumn,
//            (t1, t2) => new { Table1 = t1, Table2 = t2 })
//        .Join(context.Table3,
//            t12 => t12.Table2.OtherCommonColumn,
//            t3 => t3.OtherCommonColumn,
//            (t12, t3) => new { t12.Table1, t12.Table2, Table3 = t3 })
//        .Select(x => new
//        {
//            x.Table1.Column1,
//            x.Table2.Column2,
//            x.Table3.Column3
//        })
//        .ToList();



//var sds = (
//                from g in _context.groups
//                join m in _context.group_member on g.id equals m.group_id
//                where g.id == GroupId
//                select m.user_id
//            ).FirstOrDefault();
//var user_id = _context.group_member.Where(x => x.id == GroupId).Select(x => x.user_id).FirstOrDefault();
//string user_name = _context.user_detail.Where(x => x.UserId == user_id).Select(x => x.Name).FirstOrDefault();

//var item = (
// from g in _context.groups
// join m in _context.group_member on g.id equals m.group_id
// where (g.id == GroupId)
// select new GroupResponseDto
// {
//     name = g.name,
//     image_path= g.image_path,
//     description = g.description,
//     group_rank= g.group_rank,
//     group_admin = user_name
// }).FirstOrDefault();
//return item;


//var results = _context.groups
//  .Join(_context.group_member,
//      t1 => t1.id,
//      t2 => t2.group_id,
//      (t1, t2) => new { groups = t1, group_member = t2 })
//  .Join(_context.user_detail,
//      t12 => t12.group_member.user_id,
//      t3 => t3.UserId,
//      (t12, t3) => new { t12.groups, t12.group_member, user_detail = t3 })
//  .Select(new GroupResponseDto
//  {
//      name = t12.name,
//  }).FirstOrDefault();
//return results;