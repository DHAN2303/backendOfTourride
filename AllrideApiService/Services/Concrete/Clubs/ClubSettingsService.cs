using AllrideApiCore.Entities.Clubs;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Clubs
{
    public class ClubSettingsService : IClubSettingsService
    {
        private readonly IClubSettingsRepository _clubSettingsRepository;
        private readonly ILogger<ClubSettingsService> _logger;
        public ClubSettingsService(IClubSettingsRepository clubSettingsRepository, ILogger<ClubSettingsService> logger )
        {
            _clubSettingsRepository= clubSettingsRepository;
            _logger = logger;
        }

        public CustomResponse<string> AddUser(int clubId, int userId)
        {
            throw new NotImplementedException();
        }

        public CustomResponse<List<ClubMember>> DeleteUser(int clubId, int userId, List<int> memberId)
        {
            List<ErrorEnumResponse> errors = new();
            List<ClubMember> newGroupMember = new();
            try
            {
                List<ClubMember> updateClubMembers = new();
                List<int> memberIdList = new();
                // memberId group member tablsunda arayacağım
                // eğer varsa o kullanıcıyı grup_member tablosund adeaktife düşürmeliyim
                bool isGroupIdInteger = clubId.GetType() == typeof(int);
                bool isMemberIdInteger = clubId.GetType() == typeof(int);
                if (!isGroupIdInteger)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<List<ClubMember>>.Fail(errors, false);
                }
                if (!isMemberIdInteger)
                {
                    errors.Add(ErrorEnumResponse.ClubMemberIdIsNotInt);
                    return CustomResponse<List<ClubMember>>.Fail(errors, false);
                }

                var userRole = _clubSettingsRepository.GetUserClubRole(clubId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAClub);

                else if (userRole == 0 || userRole == 1)
                {
                    updateClubMembers = _clubSettingsRepository.GetClubMember(clubId, memberId);
                    if (updateClubMembers == null)
                    {
                        errors.Add(ErrorEnumResponse.ClubMemberIsNull);
                        return CustomResponse<List<ClubMember>>.Fail(errors, false);
                    }
                    foreach (var groupMember in updateClubMembers)
                    {
                        groupMember.active = 0;
                    }
                    _clubSettingsRepository.SaveChanges();

                    newGroupMember = _clubSettingsRepository.GetNewClubMember(clubId);
                    if (newGroupMember == null)
                    {
                        errors.Add(ErrorEnumResponse.ClubMemberIsNull);
                        return CustomResponse<List<ClubMember>>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToDeleteUserName);
                    return CustomResponse<List<ClubMember>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SETTINGS SERVICE DeleteUser METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<List<ClubMember>>.Success(newGroupMember, true);
        }

        public CustomResponse<string> UpdateClubName(int clubId, int userId, string newClubName)
        {

            List<ErrorEnumResponse> errors = new();
            Club club = new();
            string clubName = null;
            try
            {
                bool isIdInteger = clubId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _clubSettingsRepository.GetUserClubRole(clubId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAClub);

                else if (userRole == 0 || userRole == 1)
                {
                    club = _clubSettingsRepository.GetClubById(clubId);
                    if (club != null)
                    {
                        club.name = newClubName;
                        club.UpdatedDate = DateTime.Now;
                        _clubSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateClubName);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeClubName);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (club != null)
                {
                    clubName = club.name;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheClubNameIsNull);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SETTINGS SERVICE UpdateClubName METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<string>.Success(clubName, true);
        }

        public CustomResponse<string> UpdateBackgroundCoverImage(int clubId, int userId, string path)
        {   
            List<ErrorEnumResponse> errors = new();
            Club club = new();
            string clubBackgroundCoverImage = null;
            try
            {
                bool isIdInteger = clubId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _clubSettingsRepository.GetUserClubRole(clubId,userId);
                if (userRole<0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAClub);
                
                else if (userRole == 0 || userRole == 1)
                {
                    club = _clubSettingsRepository.GetClubById(clubId);
                    if (club != null)
                    {
                        club.backgroundCover_path = path;
                        club.UpdatedDate = DateTime.Now;
                        _clubSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateClubBackgroundCoverPhoto);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeBackgroundCoverPhotoClub);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (club != null)
                {
                    clubBackgroundCoverImage = club.backgroundCover_path;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheClubCurrentBackgroundCoverIsEmpty);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SETTINGS SERVICE UpdateBackgroundCoverImage METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<string>.Success(clubBackgroundCoverImage, true);
        }


        public CustomResponse<string> UpdateProfileImage(int clubId, int userId, string path)
        {
            List<ErrorEnumResponse> errors = new();
            Club club = new();
            string clubBackgroundCoverImage = null;
            try
            {
                bool isIdInteger = clubId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.CLubIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _clubSettingsRepository.GetUserClubRole(clubId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAClub);

                else if (userRole == 0 || userRole == 1)
                {
                    club = _clubSettingsRepository.GetClubById(clubId);
                    if (club != null)
                    {
                        club.backgroundCover_path = path;
                        club.UpdatedDate = DateTime.Now;
                        _clubSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateClubProfilePhoto);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeProfilePhotoClub);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (club != null)
                {
                    clubBackgroundCoverImage = club.backgroundCover_path;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheClubCurrenProfilePhotoIsEmpty);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" CLUB SETTINGS SERVICE UpdateProfileImage METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<string>.Success(clubBackgroundCoverImage, true);
        }
    }
}
