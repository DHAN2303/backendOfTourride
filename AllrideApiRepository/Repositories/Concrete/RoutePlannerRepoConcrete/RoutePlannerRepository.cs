using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using AllrideApiCore.Entities.RoutePlanners;
using AllrideApiRepository.Repositories.Abstract.RoutePlannerRepo;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiRepository.Repositories.Concrete.RoutePlannerRepoConcrete
{
    public class RoutePlannerRepository : IRoutePlannerRepository
    {
        protected readonly AllrideApiDbContext _context;
        public RoutePlannerRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public bool IsExistGroup(int GroupId)
        {
            return _context.groups.Any(g => g.id == GroupId);
        }
        public bool IsExistClub(int ClubId)
        {
            return _context.club.Any(c => c.Id == ClubId);
        }
        public RoutePlanner GetById(int Id)
        {
            return _context.route_planner.FirstOrDefault(x => x.Id == Id);
        }
        public bool IsExistId(int Id)
        {
            return _context.route_planner.Any(x=> x.Id == Id);
        }
        public IEnumerable<int> IsExistUsersInUserTable(IEnumerable<int> UsersList)
        {
            List<int> isExistUsersInUserTable = new();
            foreach (int UserId in UsersList)
            {
                var result = _context.user.FirstOrDefault(x => x.Id == UserId);
                if (result != null)
                {
                    isExistUsersInUserTable.Add(UserId);
                }
                else
                {
                    break;
                }
            }
            return isExistUsersInUserTable;
        }
        public int UserGetById(int userId)
        {
            return _context.user.Where(x=>x.Id == userId).Select(x=>x.Id).FirstOrDefault();
        }
        public IEnumerable<int> IsExistFollowingIdInSocialMediaFollowTable(IEnumerable<int> UsersList)
        {
            List<int> isExistUsersInSocialMediaFollow = new();
            foreach (int UserId in UsersList)
            {
                var result = _context.social_media_follows.FirstOrDefault(x => x.follower_id == UserId);
                var IsDeletedUser = _context.user.FirstOrDefault(x => x.Id == UserId);
                if (result == null || IsDeletedUser  == null|| IsDeletedUser.DeletedDate<DateTime.UtcNow || IsDeletedUser.ActiveUser == false)
                {
                    break;
                }
                else if(IsDeletedUser.Id == result.follower_id)
                {
                    isExistUsersInSocialMediaFollow.Add(UserId);
                }
                else
                {
                    break;
                }
            }
            return isExistUsersInSocialMediaFollow;
        }
        public bool AddRoutePlanner(RoutePlanner createRoutePlanner)
        {
            try
            {
                _context.route_planner.Add(createRoutePlanner);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddUsersInRoutePlanner(UsersInRoutePlanning usersInRoutePlanning)
        {
            _context.users_InRoutePlanning.Add(usersInRoutePlanning);
            _context.SaveChanges();
            var usersInRoutePlanningRegister = _context.users_InRoutePlanning.Find(usersInRoutePlanning.Id);
            if (usersInRoutePlanningRegister != null && usersInRoutePlanningRegister.Id >= 0)
                return true;
            else
                return false;

        }
        public List<UsersInRoutePlanning> GetUserInRoutePlanning(int RoutePlannerId)
        {
            return _context.users_InRoutePlanning.Where(x => x.RoutePlannerId == RoutePlannerId).ToList();
        }
        public bool AssigningTasksToUsersOnARoute(List<int> TaskId, int UsersInRoutePlanningId)
        {
            var usersInRoutePlanning = _context.users_InRoutePlanning.Where(x => x.Id==UsersInRoutePlanningId).FirstOrDefault();
            if(usersInRoutePlanning == null)
            {
                return false;
            }
            //else if(usersInRoutePlanning.TasksId != null)
            //{
            //    for(int i = 0; i< TaskId.Count; i++)
            //    {
            //        for (int j = i; j < usersInRoutePlanning.TasksId.Count; j++)
            //        {
            //            if (TaskId[j] == usersInRoutePlanning.TasksId[i])
            //            {
            //                usersInRoutePlanning.TasksId.Add(Task);
            //            }
            //        }
            //    }
            //    _context.SaveChanges();
            //    return true;
            //}
            else
            {
                usersInRoutePlanning.TasksId = TaskId;
                _context.SaveChanges();
                return true;
            }
        }
        public bool AddTasks(TasksRoutePlanner task)
        {
            var entity = _context.tasks_route_planner.Add(task).Entity;
            _context.SaveChanges();
            return _context.tasks_route_planner.OrderByDescending(x => x.Id).Any(x=>x.Id == entity.Id);

        }
        public bool Delete(int RoutePlannerId)
        {
            /// Birbiri ile ilişikili tablolardaki kayıtlar ef core kullanarak postgresql veritabanından nasıl silinir
           
            var result = _context.route_planner.Include(a => a.UsersInRoutePlannings)
                .Include(x=>x.TasksRoutePlanners)
                .FirstOrDefault(a => a.Id == RoutePlannerId);
            if (result != null)
            {
                // İlişkili tablolardaki kayıtları silmek için Cascade Delete işlemini kullanıyoruz.
                // Ana tabloyu silmek, ilişkili tablolardaki kayıtların da silinmesini sağlar.
                _context.route_planner.Remove(result);
                return true;
            }

            return false;
        }
        public List<int> SearchAddedUsers(int RoutePlannerId)
        {
            return _context.users_InRoutePlanning.Where(x=>x.RoutePlannerId==RoutePlannerId).Select(x=>x.SocialMediaFollower).ToList();
        }

        public bool DeleteUserInRoutePlanner(DeleteFriendsRoutePlanner deleteFriends)
        {
            var result =  _context.users_InRoutePlanning
                    .Where(x => x.RoutePlannerId == deleteFriends.RoutePlannerId)
                    .Where(x => x.SocialMediaFollower == deleteFriends.FriendsId)
                    .SingleOrDefault();
            if (result==null)
            {
                _context.users_InRoutePlanning.Remove(result); 
                return true;
            }
            return false;
        }

        public void SaveChanges() 
        { 
            _context.SaveChanges(); 
        }

        public bool LeaveUserInRoutePlanner(int RoutePlannerId, int UserId)
        {
            var result = _context.users_InRoutePlanning
                .FirstOrDefault(a => a.RoutePlannerId == RoutePlannerId && a.SocialMediaFollower == UserId);

            if (result != null)
            {
                _context.users_InRoutePlanning.Remove(result);
                return true;
            }

            return false;
        }

        public bool IsUserAlreadyAddedInRoutePlanner(int RoutePlannerId, int UserId)
        {
            var isUserInRoutePlanner = _context.users_InRoutePlanning
                .Where(x=>x.RoutePlannerId==RoutePlannerId)
                .Where(x=>x.SocialMediaFollower == UserId)
                .FirstOrDefault();
            if (isUserInRoutePlanner != null && isUserInRoutePlanner.Id >= 0)
                return true;
            else
                return false;
        }

        public bool CheckUserRoutePlanner(int UserId)
        {
            return _context.route_planner.Where(x=>x.UserId==UserId).Any();
        }

        public List<RoutePlanner> UserGetRoutePlanner(int UserId)
        {
            return _context.route_planner
                .Include(x => x.UsersInRoutePlannings)
                .Include(x => x.TasksRoutePlanners)
                .Where(_ => _.UserId == UserId)
                .ToList();
        }

        public bool IsExistTaskFromRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto)
        {
            return _context.tasks_route_planner
                .Any(x => x.RoutePlannerId == deleteTaskRoutePlannerDto.RoutePlannerId && x.Id == deleteTaskRoutePlannerDto.TaskId);
        }

        public bool DeleteTaskInRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto)
        {
            var result = _context.tasks_route_planner
                .FirstOrDefault(a => a.Id == deleteTaskRoutePlannerDto.TaskId);

            if (result != null)
            {
                _context.tasks_route_planner.Remove(result);
                return true;
            }

            return false;
        }

    }
}


    
//public bool LeaveUserInRoutePlanner(int RoutePlannerId, int UserId)
//{
//    //throw new NotImplementedException();

//    var result = _context.route_planner
//        .Include(a => a.UsersInRoutePlannings)
//        .Where(a => a.Id == RoutePlannerId)
//        .Where(a => a.UserId == UserId)
//        .FirstOrDefault();
//    if (result != null)
//    {
//        _context.route_planner.Remove(result);
//        return true;
//    }

//    return false;
//}

//public List<RoutePlanner> UserGetRoutePlanner(int UserId)
//{
//    return _context.route_planner
//        .Include(x=>x.UsersInRoutePlannings)
//        .Include(x=>x.TasksRoutePlanners)
//        .Where(_ => _.UserId==UserId)
//        .ToList();
//}


//public bool AddFriendsRoutePlanner(AddPlanedRouteUsers addPlanedUsers, int UserId)
//{
//    try
//    {
//        _context.planed_route_users.Add(addPlanedUsers);
//        return true;
//    }
//    catch (Exception ex)
//    {
//        return false;
//    }
//}

//var result = (
//     from r in _context.route_planner
//     join urp in _context.users_InRoutePlanning on r.Id equals urp.RoutePlannerId
//     join trp in _context.tasks_route_planner on r.Id equals trp.RoutePlannerId
//     where (r.UserId == UserId)
//     select new UserHaveRoutePlannerResponseDto
//     {
//         RoutePlannerTitle = r.RoutePlannerTitle,
//         RouteName = r.RouteName,
//         StartDate = r.StartDate,
//         EndDate = r.EndDate,
//         ColorCodeHex = r.ColorCodeHex,
//         RouteAlertTime = r.RouteAlertTime,
//         Tasks = trp.Tasks == null ? "Task Is Null" : trp.Tasks,
//         SocialMediaFollower = urp.SocialMediaFollower

//     }).ToList();

//return result;