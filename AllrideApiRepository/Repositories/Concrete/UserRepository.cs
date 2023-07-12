using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {

        protected readonly AllrideApiDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AllrideApiDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<UserProfileResponseDto> GetClubsUsers(int UserId)
        {
            //return _context.group_member.Where(x=>x.group_id == GroupId).Select(x=>x.user_id).FirstOrDefault();

            var userList = (
                  from u in _context.user
                  join ud in _context.user_detail on u.Id equals ud.UserId
                  where u.Id == UserId
                  select new UserProfileResponseDto
                  {
                      Name = ud.Name,
                      LastName = ud.LastName,
                      PpPath = ud.PpPath
                  }
                  ).ToList();
            return userList;
        }
        public UserEntity Add(UserEntity user)
        {
            return _context.user.Add(user).Entity;
        }
        public UserDetail AddUserDetail(UserDetail userDetail)
        {
            var isUserRegister = _context.user_detail.Add(userDetail).Entity;
            return isUserRegister;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public UserEntity GetUserWithUserDetail(UserEntity user)
        {
            //_context..Include(x => x.UserDetail).
            return _context.user.Include(x => x.UserDetail).SingleOrDefault(x => x.Email == user.Email);  // Bu sorgu hem Userı hemde usera bağlı user detail bilgilerini çekiyor
        }
        public bool IsExistUserEmail(UserEntity user)
        {
            return _context.user.Any(u => u.Email == user.Email);
        }
        public bool IsExistUserPhone(string phone)
        {
            return _context.user_detail.Any(ud => ud.Phone == phone);
        }

        public bool Update(string VehicleType, int userId)
        {
            try
            {
                var result = _context.user_detail.SingleOrDefault(x => x.UserId == userId);
                if (result == null)
                {
                    return false;
                }
                result.VehicleType = VehicleType;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message + " " + e.InnerException);
                return false;
            }
            return true;
        }

        public IList<UserDetail> GetAll()
        {
            return _context.user_detail.ToList();
        }

        public List<Message> GetUserFriendsLastMessage(int UserId)
        {
            return _context.messages.OrderByDescending(m => m.created_at).Take(UserId).ToList();
        }

        public List<Message> GetPeerToPeerMessage(int userId, int clientId)
        {
            return _context.messages.Where(m => m.sender_id == userId || m.recipient_id == userId).ToList();
        }

        public List<GroupMessage> GetGroupMessage(int groupId)
        {
            return _context.group_messages.Where(m => m.group_id == groupId).ToList();
        }

        public List<UserDetail> GetMessagedUser(int userId)
        {
            return _context.messages
            .Where(m => m.sender_id == userId || m.recipient_id == userId)
            .Join(
                _context.user_detail,
                m => m.sender_id == userId ? m.recipient_id : m.sender_id,
                u => u.UserId,
                (m, u) => new { Message = m, UserDetail = u }
            )
            .Select(j => new UserDetail
            {
                UserId = j.UserDetail.UserId,
                Name = j.UserDetail.Name,
                LastName = j.UserDetail.LastName,
            }).Distinct()
            .ToList();
        }
        public List<UserDetail> GetGroupMessagedUser(int groupId)
        {
            return _context.group_messages
            .Where(m => m.group_id == groupId)
            .Join(
                _context.user_detail,
                m => m.sender_id,
                u => u.UserId,
                (m, u) => new { Message = m, UserDetail = u }
            )
            .Select(j => new UserDetail
            {
                UserId = j.UserDetail.UserId,
                Name = j.UserDetail.Name,
                LastName = j.UserDetail.LastName,
            }).Distinct()
            .ToList();
        }

        public List<UserDetail> GetSearchUser(string input)
        {
            return _context.user_detail.Where(x => x.Equals(input)).ToList();
        }

        // 13 Haziran
        public List<UserProfileResponseDto> GetFollowersUsers(int UserId)
        {
            var userList = (
                   from u in _context.user
                   join ud in _context.user_detail on u.Id equals ud.UserId
                   join smf in _context.social_media_follows on ud.UserId equals smf.follower_id
                   where u.Id == UserId
                   select new UserProfileResponseDto
                   {
                       Name = ud.Name,
                       LastName = ud.LastName,
                       PpPath = ud.PpPath
                   }
                   ).ToList();
            return userList;
        }
        // 13 Haziran
        public List<UserProfileResponseDto> GetFollowingUsers(int UserId)
        {
            var userList = (
                   from u in _context.user
                   join ud in _context.user_detail on u.Id equals ud.UserId
                   join smf in _context.social_media_follows on ud.UserId equals smf.followed_id
                   where u.Id == UserId
                   select new UserProfileResponseDto
                   {
                       Name = ud.Name,
                       LastName = ud.LastName,
                       PpPath = ud.PpPath
                   }
                   ).ToList();
            return userList;
        }
    }
}

// GetUser CASE
//public User GetUser(User user)
//{

//    return _context.user.SingleOrDefault(x => x.Email == user.Email);  // Bu sorgu hem Userı hemde usera bağlı user detail bilgilerini çekiyor


//    //var userId =  _context.user.Where(x => x.email == user.email).FirstOrDefault();

//    //Debug.WriteLine(_context.user.Include(x => x.userdetail).SingleOrDefault(x => x.email == user.email));

//    //return _context.Users.SingleOrDefault(x => x.PhoneNumber == user.PhoneNumber);
//    //https://stackoverflow.com/questions/4660142/what-is-a-nullreferenceexception-and-how-do-i-fix-it
//}

//public User GetUserWithPassword(User user)
//{
//    return _context.user.Include(x => x.userpassword).SingleOrDefault(x => x.email == user.email);
//}


//public void Delete(UserDetail user)
//{
//    _context.user.Remove(user);
//}


//public UserDetail Update(UserDetail user)
//{
//    return _context.user_detail.Update(user).Entity;
//}


//public UserDetail Get(UserDetail user)
//{
//    return _context.user_detail.SingleOrDefault(x => x.Email == user.Email);
//}

// case 2: 

//public bool GetUserMailPhone(User user)
//{
//    // return _context.Users.Single(x => x.Email == user.Email || x.PhoneNumber == user.PhoneNumber);
//     return _context.Users.Contains(user);
//    //return _context.Users.SingleOrDefault(x => x.PhoneNumber == user.PhoneNumber);
//    //https://stackoverflow.com/questions/4660142/what-is-a-nullreferenceexception-and-how-do-i-fix-it
//}

//case 3:

//public bool GetUserMailPhone(User user)
//{

//    var isThereUser = _context.Users.Where(x => x.Email == user.Email || x.PhoneNumber == user.PhoneNumber).Count();
//    if (isThereUser < 1)
//    {
//        return false;
//    }
//    return true;

//    //return _context.Users.SingleOrDefault(x => x.PhoneNumber == user.PhoneNumber);
//    //https://stackoverflow.com/questions/4660142/what-is-a-nullreferenceexception-and-how-do-i-fix-it
//}

//public void Add1(/*User user*/)
//{
//    _context.Database.ExecuteSqlRawAsync("insert into users1 (geometry) values(ST_AsText( ST_MakeLine(ST_Point(36.995624117136025, 34.31738383468312), ST_Point(36.814614, 34.651682)) ))");

//}
