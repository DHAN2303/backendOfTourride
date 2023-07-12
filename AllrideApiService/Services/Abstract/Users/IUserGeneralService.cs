using AllrideApiCore.Dtos.Select;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface IUserGeneralService
    {
        public CustomResponse<IList<UserGeneralDto>> GetAll();
    }
}
