using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiRepository.Repositories.Abstract.Clubs;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Nest;

namespace AllrideApiService.Services.Concrete
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> _logger;
        private readonly IClubRepository _clubRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public SearchService(ILogger<SearchService> logger, IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository= clubRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public CustomResponse<List<ClubResponseDto>> GetClub(string input)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            List<ClubResponseDto> clubResponseDtoList = new();
            try
            {
                var clubList = _clubRepository.SearchClub(input);
                if(clubList.Count<0 || clubList == null)
                {
                    errors.Add(ErrorEnumResponse.ClubDntRegisterInDB);
                    return CustomResponse<List<ClubResponseDto>>.Fail(errors, false);
                }
                foreach (var item in clubList)
                {
                    ClubResponseDto clubResponseDto = _mapper.Map<ClubResponseDto>(item);
                    if (item == null)
                        continue;
                    clubResponseDtoList.Add(clubResponseDto);
                }
                if (clubList == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<ClubResponseDto>>.Fail(errors, false);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(" SEARCH SERVICE GET CLUB ERROR GETCLUB METHOD " + ex.Message);
            }

            return CustomResponse<List<ClubResponseDto>>.Success(clubResponseDtoList, true);
        }

        public CustomResponse<List<GroupResponseDto>> GetGroup(string input)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            List<GroupResponseDto> groupResponseDtoList = new();
            try
            {
                var groupList = _groupRepository.SearchGroup(input);
                if (groupList.Count < 0 || groupList == null)
                {
                    errors.Add(ErrorEnumResponse.GroupDntRegisterInDB);
                    return CustomResponse<List<GroupResponseDto>>.Fail(errors, false);
                }
                foreach (var item in groupList)
                {
                    GroupResponseDto groupResponseDto = _mapper.Map<GroupResponseDto>(item);
                    if (item == null)
                        continue;
                    groupResponseDtoList.Add(groupResponseDto);
                }
                if (groupList == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<GroupResponseDto>>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" SEARCH SERVICE GET CLUB ERROR GETCLUB METHOD " + ex.Message);
            }

            return CustomResponse<List<GroupResponseDto>>.Success(groupResponseDtoList, true);
        }
    }
}
