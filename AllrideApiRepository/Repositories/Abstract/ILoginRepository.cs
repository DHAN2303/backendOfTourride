using AllrideApiCore.Entities.Users;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface ILoginRepository
    {

        UserEntity GetUserWithPassword(UserEntity user);
        UserEntity GetUser(UserEntity user);
        UserEntity ForgotPassword(UserEntity user);
        UserEntity GetUserById(int id);
        UserDetail GetUserDetail(UserEntity user);
        //Task<User> ForgotPassword(UserUpdate userPassword);
    }
}
