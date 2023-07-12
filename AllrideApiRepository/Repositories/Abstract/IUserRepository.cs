using AllrideApiCore.Dtos.ResponseDtos;
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
        public List<GroupMessage> GetGroupMessage(int groupId);
        public List<UserDetail> GetGroupMessagedUser(int groupId);
        public List<UserDetail> GetSearchUser(string input);
        public List<UserProfileResponseDto> GetFollowersUsers(int UserId);
        public List<UserProfileResponseDto> GetFollowingUsers(int UserId);



    }
}




//UserDetail GetUserDetail(UserDetail uDetail, int userId);
//User GetUserWithPassword(User user);
//UserDetail Update(UserDetail user);
//UserDetail Get(UserDetail user);