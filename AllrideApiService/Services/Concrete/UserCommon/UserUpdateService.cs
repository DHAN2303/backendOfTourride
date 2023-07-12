using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.Users;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.UserCommon
{
    public class UserUpdateService : IUserUpdateService
    {
        protected AllrideApiDbContext _context;
        private readonly ILogger<UserUpdateService> _logger;
        public UserUpdateService(AllrideApiDbContext context, ILogger<UserUpdateService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public object UpdateUser(UserUpdate userDetail)
        {
            var user_id = userDetail.UserId;
            var name = userDetail.Name;
            var surname = userDetail.LastName;
            var dateOfBirth = userDetail.DateOfBirth;
            var gender = userDetail.Gender;
            var phone = userDetail.Phone;
            var country = userDetail.Country;
            var language = userDetail.Language;
            var PpPath = userDetail.PpPath;

            try
            {
                var user_detail = _context.user_detail.FirstOrDefault(u => u.UserId == user_id);
                var user = _context.user.FirstOrDefault(u => u.Id == user_id);

                if (user != null && user_detail != null)
                {
                    user_detail.Name = name != null ? name : user_detail.Name;
                    user_detail.LastName = surname != null ? surname : user_detail.LastName;
                    user_detail.DateOfBirth = (DateTime)(dateOfBirth != null ? dateOfBirth : user_detail.DateOfBirth);
                    user_detail.Gender = gender != null ? gender : user_detail.Gender;
                    user_detail.Phone = phone != null ? phone : user_detail.Phone;
                    user_detail.Country = country != null ? country : user_detail.Country;
                    user_detail.Language = language != null ? language : user_detail.Language;
                    user_detail.PpPath = PpPath != null ? PpPath : user_detail.PpPath;

                    user.UpdatedDate = DateTime.UtcNow;

                    _context.SaveChanges();
                    return null;
                }
                else return "false";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while updating user: " + ex.Message);
                return  "false";
            }

        }



        public object FetcUserData(int userId)
        {
            var user = _context.user_detail.FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
