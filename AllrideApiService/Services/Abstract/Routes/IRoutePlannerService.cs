using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using AllrideApiCore.Dtos.ResponseDto.RoutePlannerResponseDto;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Routes
{
    public interface IRoutePlannerService
    {
        public CustomResponse<NoContentDto> SaveRoutePlanner(CreateRoutePlannerDto routePlanner, int UserId);
        public CustomResponse<List<int>> AddFriendsRoutePlanner (AddFriendsRoutePlannerDto routePlannerId, int UserId);
        public CustomResponse<List<int>> AssigningTasksToUsersOnARoute(AddFriendsTasksRoutePlannerDto addFriendsTasksRoutePlanner, int UserId);
        public CustomResponse<NoContentDto> AddTasks(CreateTasksInRoutePlanner createTasks, int UserId);
        public CustomResponse<NoContentDto> DeleteRoutePlanner(int RoutePlannerIdint, int UserId);
        public CustomResponse<NoContentDto> UserLeaving(int RoutePlannerId, int UserId);
        public CustomResponse<List<UserHaveRoutePlannerResponseDto>> GetAllRoutePlanner(int UserId);
        public CustomResponse<NoContentDto> DeleteTaskFromRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto);
        //public CustomResponse<NoContentDto> DeleteUserForRoutePlanner(DeleteFriendsRoutePlanner deleteFriendsRoutePlanner, int userId);
    }
}






















