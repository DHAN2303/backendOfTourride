using AllrideApiCore.Entities.Users;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract
{
    public interface ITokenHandler
    {
        BaseResponse CreateAccessToken(UserEntity user, UserDetail userDetail);
    }
}
