using AllrideApiCore.Entities.Users;

namespace AllrideApiService.Services.Abstract.Users
{
    public interface IUserUpdateService
    {
        public object UpdateUser(UserUpdate userDetail);

        public object FetcUserData(int userId);


    }
}
