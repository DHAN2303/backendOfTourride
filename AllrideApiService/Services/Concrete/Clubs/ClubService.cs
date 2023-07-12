using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
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
                _logger.LogError(" CLUB SERVICE DeleteGroup METHOD LOG ERROR " + ex.Message);
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
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<GlobalClubResponseDto>.Fail(errors, false);
                }
                if (UserId < 0)
                {
                    errors.Add(ErrorEnumResponse.ClubAdminIsNull);
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
                _logger.LogError(" CLUB SERVICE DeleteUserInGroup METHOD LOG ERROR " + ex.Message);
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
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
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
                _logger.LogError(" CLUB SERVICE GET GROUP MEDIA METHOD LOG ERROR " + ex.Message);
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
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
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
                _logger.LogError("CLUB SERVICE GET CLUB MEDIA METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<ClubResponseDto>.Success(clubResponse, true);
        }

        public CustomResponse<UserResponseDto> GetClubUserDetail(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            UserResponseDto userDetail = null;
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<UserResponseDto>.Fail(errors, false);
                }
                if (_clubRepository.IsExistClub(ClubId))
                {
                    userDetail = _clubRepository.GetClubUserDetail(ClubId);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<UserResponseDto>.Fail(errors, false);
                }

                if (userDetail == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<UserResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<UserResponseDto>.Success(userDetail, true);
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

                if (groupResponse == null || groupResponse.Count < 1)
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
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
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

        public CustomResponse<LastActivityResponseDto> GetLastActivity(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            LastActivityResponseDto lastActivity = new();
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = ClubId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<LastActivityResponseDto>.Fail(errors, false);
                }
                lastActivity = _clubRepository.GetLastActivity(ClubId);
                if (lastActivity == null)
                {
                    errors.Add(ErrorEnumResponse.ActivityDontRegisterDB);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SERVICE GetLastActivity ERROR:  " + ex.Message);
            }
            return CustomResponse<LastActivityResponseDto>.Success(lastActivity, true);

        }

        // 29 MAYIS
        public CustomResponse<List<ClubResponseDto>> GetUsersClubList(int userId)
        {
            List<ErrorEnumResponse> errors = new();
            List<ClubResponseDto> clubResponseDto = new();
            try
            {
                clubResponseDto = _clubRepository.GetUsersClubList(userId);
                if (clubResponseDto == null)
                {
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SERVICE GetUsersClubList ERROR:  " + ex.Message);
            }
            return CustomResponse<List<ClubResponseDto>>.Success(clubResponseDto, true);
        }

        public CustomResponse<List<ClubSocialMediaPostsResponseDto>> GetClubsUsersSocialMediaLast3Post(int clubId)
        {
            List<ErrorEnumResponse> errors = new();
            List<ClubSocialMediaPostsResponseDto> clubSocialPostWithCommentList = new();
            try
            {
                if (clubId < 0 || clubId is int == false)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<List<ClubSocialMediaPostsResponseDto>>.Fail(errors, false);
                }

                clubSocialPostWithCommentList = _clubRepository.GetClubUsersSocialMediaLast3Post(clubId);
                if (clubSocialPostWithCommentList.Count < 0)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoPostSharedInTheClub);
                    return CustomResponse<List<ClubSocialMediaPostsResponseDto>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SERVICE GetClubsUsersSocialMediaLast3Post METHOD ERROR:  " + ex.Message);
            }
            return CustomResponse<List<ClubSocialMediaPostsResponseDto>>.Success(clubSocialPostWithCommentList, true);
        }

        public CustomResponse<int> GetClubAdminNumber(int ClubId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (ClubId < 0)
                {
                    errors.Add(ErrorEnumResponse.ClubIdCannotBeLessThan0);
                    return CustomResponse<int>.Fail(errors, false);
                }

                var adminCount = _clubRepository.GetClubMemberCount(ClubId);
                if (adminCount < 0)
                {
                    errors.Add(ErrorEnumResponse.ClubAdminIsNull);
                    return CustomResponse<int>.Fail(errors, false);
                }

                return CustomResponse<int>.Success(adminCount, true);
            }
            catch (Exception ex)
            {
                errors.Add(ErrorEnumResponse.ApiServiceFail);
                _logger.LogError(" CLUB SERVICE GetClubAdminNumber METHOD ERROR:  " + ex.Message);
                return CustomResponse<int>.Fail(errors, false);
            }

        }

        // 13 Haziran
        public CustomResponse<List<UserProfileResponseDto>> GetFollowers(int clubId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (clubId < 0)
                {
                    errors.Add(ErrorEnumResponse.ClubIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
                }

                var result = _clubRepository.GetClubsUsers(clubId);
                if (result == null)
                {
                    errors.Add(ErrorEnumResponse.ClubMemberIsNull);
                    return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
                }

                return CustomResponse<List<UserProfileResponseDto>>.Success(result, true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " ClubService -->  GetFollowers METHOD ERROR: " + ex.InnerException.ToString());
                return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
            }
        }

        CustomResponse<UserResponseDto> IClubService.GetClubUserDetail(int ClubId)
        {
            throw new NotImplementedException();
        }
    }
}
