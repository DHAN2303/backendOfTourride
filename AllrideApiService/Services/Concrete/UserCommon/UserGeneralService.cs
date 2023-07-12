using AllrideApiCore.Dtos.Select;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Users;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.UserCommon
{
    public class UserGeneralService : IUserGeneralService
    {
        private readonly IUserRepository _userGeneralRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGeneralService> _logger;
        public UserGeneralService(IUserRepository userGeneralRepository, IMapper mapper, ILogger<UserGeneralService> logger)
        {
            _userGeneralRepository = userGeneralRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public CustomResponse<IList<UserGeneralDto>> GetAll()
        {
            // var userGeneralDto = new();
            List<UserGeneralDto> userGeneralDtos = new List<UserGeneralDto>();
            List<ErrorEnumResponse> enumResponse = new List<ErrorEnumResponse>();
            try
            {
                var result = _userGeneralRepository.GetAll();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var userGeneralDto = _mapper.Map<UserGeneralDto>(item);

                        userGeneralDtos.Add(userGeneralDto);
                    }
                }
                else
                {
                    enumResponse.Add(ErrorEnumResponse.NoRegisteredUsersInDb);
                    return CustomResponse<IList<UserGeneralDto>>.Fail(enumResponse, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("User General Service Error Log Kaydı: " + ex.Message);
            }

            return CustomResponse<IList<UserGeneralDto>>.Success(userGeneralDtos, true);
        }
    }
}
