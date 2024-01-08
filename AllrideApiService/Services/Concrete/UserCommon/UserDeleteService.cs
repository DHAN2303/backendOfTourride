using AllrideApiCore.Entities.Users;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Authentication;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Users;
using AutoMapper;
using DTO.Select;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.UserCommon
{
    public class UserDeleteService : IUserDeleteService
    {
        protected readonly AllrideApiDbContext _context;
        private const string V = "User Delete Servis Katmanındaki Error Log: ";
        private LoginService _loginService;
        private readonly IMapper _mapper;
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<UserDeleteService> _logger;


        public UserDeleteService(AllrideApiDbContext context, ILoginRepository loginRepository, IMapper mapper, ILogger<UserDeleteService> logger)
        {
            _context = context;
            _logger = logger;
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        public object UserDelete(LoginUserDto userDto)
        {
            var verifyPass = VerifyPassword(userDto);
            if (verifyPass.Status)
            {
                var user = _context.user.FirstOrDefault(x => x.Email == userDto.Email);
                if (user != null)
                    try
                    {
                        //_context.Remove(user);
                        _context.user
                            .Where(e => e.Email == userDto.Email)
                            .ToList()
                            .ForEach(e =>
                            {
                                e.ActiveUser = false;
                                e.DeletedDate = DateTime.Now;
                            });

                        _context.SaveChanges();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("When deleteing user error: " + ex.ToString());
                        return ex.Message;
                    }
                else return "false";
            }
            else
            {
                return "false";
            }

        }


        public CustomResponse<UserEntity> VerifyPassword(LoginUserDto userDto)
        {
            UserEntity user = _mapper.Map<UserEntity>(userDto);
            var registeredUser = GetUser(user);
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            try
            {

                var result = HashHelper.VerifyPasswordHash(userDto.Password, registeredUser.UserPassword.HashPass, registeredUser.UserPassword.SaltPass);
                if (registeredUser == null || registeredUser.Email == null || result == false)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.UserLoginFailed);
                    return CustomResponse<UserEntity>.Fail(_enumListErrorResponse, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(V + " " + ex.Message);
            }
            return CustomResponse<UserEntity>.Success(registeredUser, true);
        }
        public UserEntity GetUser(UserEntity user)
        {
            user = _loginRepository.GetUserWithPassword(user);
            return user;
        }
    }
}
