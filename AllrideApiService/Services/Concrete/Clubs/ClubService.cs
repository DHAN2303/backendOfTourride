using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Clubs;
using AllrideApiRepository.Repositories.Abstract.Clubs;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.ClubsInfo;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AllrideApiService.Services.Concrete.Clubs
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ClubService> _logger;
        public ClubService(IClubRepository clubRepository, IMapper mapper, ILogger<ClubService> logger)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
            _logger = logger;
        }

     
        public CustomResponse<GlobalClubResponseDto> DeleteClub(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }
                if (_clubRepository.IsExistClub(ClubId))
                {
                    bool isGroupDeleted = _clubRepository.DeleteClub(ClubId);
                    return CustomResponse<GlobalClubResponseDto>.Success("" + ClubId, true);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE DeleteGroup METHOD LOG ERROR " + ex.Message);
                return CustomResponse<GlobalClubResponseDto>.Success("" + ClubId, false);

            }

        }

        public CustomResponse<GlobalClubResponseDto> DeleteUserInClub(int ClubId, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }
                if (UserId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }

                if (_clubRepository.IsExistUserInClub(ClubId, UserId))
                {
                    bool isGroupDeleted = _clubRepository.DeleteUserInClub(ClubId, UserId);
                    return CustomResponse<GlobalClubResponseDto>.Success("ClubID:" + ClubId + " UserId:" + UserId, true);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE DeleteUserInGroup METHOD LOG ERROR " + ex.Message);
                return CustomResponse<GlobalClubResponseDto>.Success("GroupID:" + ClubId + " UserId:" + UserId, false);

            }
        }

        public CustomResponse<ClubResponseDto> GetClubDetail(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            ClubResponseDto club = null;
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<ClubResponseDto>.Fail(errors, false);
                }

                club = _clubRepository.GetClubDetail(ClubId);
                if (club == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<ClubResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<ClubResponseDto>.Success(club, true);
        }

        public CustomResponse<ClubResponseDto> GetClubMedia(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            ClubResponseDto clubResponse = new();
            try
            {
                if (ClubId < 0 && ClubId is int == false)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<ClubResponseDto>.Fail(errors, false);
                }

                var club = _clubRepository.GetMedia(ClubId);
                if (club == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<ClubResponseDto>.Fail(errors, false);
                }
                clubResponse = _mapper.Map<ClubResponseDto>(club);
                if (clubResponse == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<ClubResponseDto>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GET GROUP MEDIA METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<ClubResponseDto>.Success(clubResponse, true);
        }

        public CustomResponse<Object> GetClubUserDetail(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            List<UserResponseDto> userDetail = null;
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<Object>.Fail(errors, false);
                }
                if (_clubRepository.IsExistClub(ClubId))
                {
                    userDetail = _clubRepository.GetClubUserDetail(ClubId);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<Object>.Fail(errors, false);
                }

                if (userDetail == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<Object>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<Object>.Success(userDetail, true);
        }

        public CustomResponse<GlobalClubResponseDto> GetGlobalClubs(int ClubId, int Type)
        { 
            List<ErrorEnumResponse> errors = new();
            List<GlobalClubResponseDto> groupResponse = null; ;
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }

                groupResponse = _clubRepository.GetGlobalClubs(ClubId, Type);

                if (groupResponse == null || groupResponse.Count<1)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGlobalGroups METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<GlobalClubResponseDto>.Success(groupResponse[0], true);
        }

        public CustomResponse<List<string>> SearchUserClub(string userName, int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            List<string> users = new();
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = ClubId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }

                if (userName.IsNullOrEmpty())
                {
                    errors.Add(ErrorEnumResponse.UserIdCannotBeNullOrEmptyInClub);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }
                users = _clubRepository.SearchUserClub(userName, ClubId);
                if (users == null || users.Count < 1)
                {
                    errors.Add(ErrorEnumResponse.SearchedUserNotFoundinGroup);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE SearchGroup ERROR:  " + ex.Message);
            }

            return CustomResponse<List<string>>.Success(users, true);
        }


        public CustomResponse<Object> GetMemberedClubsByUser(int userId)
        {
            List<ErrorEnumResponse> errors = new();
            List<Club> clubs = null;
            try
            {
                if (userId < 0)
                {
                    errors.Add(ErrorEnumResponse.NoUserIdInUserDetailTable);
                    return CustomResponse<Object>.Fail(errors, false);
                }

                clubs = _clubRepository.GetClubsForUser(userId);
                if (clubs == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<Object>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<Object>.Success(clubs, true);
        }

        public CustomResponse<Object> GetClubMessage(int clubId)
        {
            var response = _clubRepository.GetClubMessage(clubId);
            if(response != null)
            {
                return CustomResponse<Object>.Success(response, true);
            }
            else
            {
                return CustomResponse<Object>.Success(response, false);
            }
        }
    }
}
