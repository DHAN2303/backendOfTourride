using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiRepository.Repositories.Concrete
{

    public class LoginRepository : ILoginRepository
    {
        protected readonly AllrideApiDbContext _context;
        public LoginRepository(AllrideApiDbContext context)
        {
            _context = context;
        }
        public UserEntity GetUser(UserEntity user)
        {
            return _context.user.SingleOrDefault(x => x.Email == user.Email);  // Bu sorgu hem Userı hemde usera bağlı user detail bilgilerini çekiyor
        }
        public UserEntity GetUserById(int id)
        {
            return _context.user.SingleOrDefault(x => x.Id == id);
        }
        public UserEntity GetUserWithPassword(UserEntity user)
        {
            return _context.user.Include(x => x.UserPassword).SingleOrDefault(x => x.Email == user.Email);
        }

        public UserEntity ForgotPassword(UserEntity user)
        {
            return _context.user.SingleOrDefault(x => x.Email == user.Email);
        }

        public UserDetail GetUserDetail(UserEntity user)
        {
            return _context.user_detail.SingleOrDefault(x => x.UserId == user.Id);
            // return _context.user.Include(x => x.UserDetail).SingleOrDefault(x => x.Id == user.Id);  // User datası dönüyor
        }

        //public async Task<User> ForgotPassword(UserUpdate userPassword)
        //{
        //    return await _context.user.SingleOrDefaultAsync(x => x.Email == userPassword.Email);
        //}
    }
}

//   https://www.google.com/search?q=viski&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjI4MaAmub8AhXVi8MKHQhDAhQQ_AUoAXoECAEQAw&biw=1536&bih=722&dpr=1.25#imgrc=EpKnDtoM4cuPWM

