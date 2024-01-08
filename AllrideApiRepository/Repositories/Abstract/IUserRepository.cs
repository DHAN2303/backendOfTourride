using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Users;

namespace AllrideApiRepository.Repositories.Abstract
{
    public interface IUserRepository
    {
        UserEntity Add(UserEntity user);
        UserDetail AddUserDetail(UserDetail userDetail);
        void SaveChanges();
        UserEntity GetUserWithUserDetail(UserEntity user);
        public bool IsExistUserEmail(UserEntity user);
        public bool IsExistUserPhone(string phone);
        public bool Update(string VehicleType, int userId);
        public IList<UserDetail> GetAll();
        public List<Message> GetUserFriendsLastMessage(int UserId);
        public List<UserDetail> GetMessagedUser(int userId);
        public List<Message> GetPeerToPeerMessage(int userId, int clientId);


    }
}



//UserDetail GetUserDetail(UserDetail uDetail, int userId);
//User GetUserWithPassword(User user);
//UserDetail Update(UserDetail user);
//UserDetail Get(UserDetail user);