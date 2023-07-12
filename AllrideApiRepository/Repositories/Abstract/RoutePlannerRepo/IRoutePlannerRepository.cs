using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using AllrideApiCore.Dtos.ResponseDto.RoutePlannerResponseDto;
using AllrideApiCore.Entities.RoutePlanners;

namespace AllrideApiRepository.Repositories.Abstract.RoutePlannerRepo
{
    public interface IRoutePlannerRepository
    {
        public RoutePlanner GetById(int Id);
        public bool IsExistId(int Id);
        public bool AddUsersInRoutePlanner(UsersInRoutePlanning usersInRoutePlanning);
        public IEnumerable<int> IsExistUsersInUserTable(IEnumerable<int> UsersList);
        public IEnumerable<int> IsExistFollowingIdInSocialMediaFollowTable(IEnumerable<int> UsersList);
        public bool AddRoutePlanner(RoutePlanner createRoutePlanner);
        public bool AssigningTasksToUsersOnARoute(List<int> TaskId, int UsersInRoutePlanningId);
        public bool IsExistGroup(int GroupId);
        public bool IsExistClub(int ClubId);
        public bool AddTasks(TasksRoutePlanner task);
        public bool Delete(int RoutePlannerId);
        public List<int> SearchAddedUsers(int RoutePlannerId);
        public List<UsersInRoutePlanning> GetUserInRoutePlanning(int RoutePlannerId);
        public int UserGetById(int userId);
        public bool LeaveUserInRoutePlanner(int RoutePlannerId, int UserId);
        public bool IsUserAlreadyAddedInRoutePlanner(int RoutePlannerId, int UserId);
        // public bool IsAddedUserInRoutePlanner(int RoutePlannerId, int UserId);
        public void SaveChanges();
        public bool CheckUserRoutePlanner(int UserId);
        //public List<UserHaveRoutePlannerResponseDto> UserGetRoutePlanner(int UserId);
        public List<RoutePlanner> UserGetRoutePlanner(int UserId);
        public bool IsExistTaskFromRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto);
        public bool DeleteTaskInRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto);

    }
}
