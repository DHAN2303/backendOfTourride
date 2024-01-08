using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete.GroupsClubs
{
    public class GroupRepository : IGroupRepository // <T> : IGroupClubBaseRepository<T>
    {
        private readonly AllrideApiDbContext _context;
        private readonly ILogger<GroupRepository> _logger;
        public GroupRepository(AllrideApiDbContext context, ILogger<GroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool DeleteUserInGroup(int GroupId, int UserId)
        {
            try
            {
                var expiredStories = _context.group_member.FirstOrDefault(e => e.group_id == GroupId&& e.user_id == UserId);
                _context.Remove(expiredStories);
                _context.SaveChanges();
                checkGroupAdmin(GroupId);
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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }

        public bool IsExistGroup(int GroupId)
        {
            return _context.groups.Any(e => e.id == GroupId);
        }


        public List<GlobalGroupResponseDto>  GetGlobalGroups(int GroupId, int Type)
        {
            using (_context)
            {
                return _context.groups
                    .Where(e => e.id== GroupId && e.type==Type)
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
                    Id = g.id,
                    name = g.name,
                    image_path= g.image_path,
                    description= g.description,
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
                        image_path = e.image_path,
                        description = e.description
                    }).ToList();
            }
        }

        public List<UserResponseDto> GetGroupUserDetail(int GroupId)
        {
            var item = (
              from g in _context.group_member
              join u in _context.user_detail on g.user_id equals u.Id
              where (g.group_id == GroupId)
              select new UserResponseDto
              {
                  Id = u.UserId,
                  Name=u.Name,
                  LastName=u.LastName,
                  DateOfBirth = u.DateOfBirth,
                  Gender = u.Gender,
                  Phone = u.Phone,
                  Country = u.Country,
                  Language = u.Language,
                  PpPath = u.PpPath
              }).ToList();
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



        public List<Group> GetGroupsForUser(int userId)
        {
           var groups = _context.group_member
               .Where(gm => gm.user_id == userId)
               .Join(_context.groups,
                   gm => gm.group_id,
                   g => g.id,
                   (gm, g) => g)
               .ToList();
           return groups;
        }

        public List<GroupMessage> GetGroupMessage(int groupId)
        {
            return _context.group_messages.Where(m => m.group_id == groupId).ToList();
        }

        public void checkGroupAdmin(int groupId)
        {
            var members = _context.group_member.Where(member => member.group_id == groupId).ToList();

            if (members.Count > 0)
            {
                var response = _context.group_member.Any(member =>
                member.group_id == groupId && member.role == 0);
                
                if (!response)
                {
                    members[0].role = 0;
                    _context.SaveChanges();
                }

            }
            else
            {
                var group = _context.groups.FirstOrDefault(b => b.id == groupId);
                _context.groups.Remove(group);
                _context.SaveChanges();
            }
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