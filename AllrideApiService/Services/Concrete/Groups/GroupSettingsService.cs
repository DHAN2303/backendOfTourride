using AllrideApiCore.Entities.Groups;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Groups
{
    public class GroupSettingsService : IGroupSettingsService
    {
        private readonly IGroupSettingsRepository _groupSettingsRepository;
        private readonly ILogger<GroupSettingsService> _logger;
        protected readonly AllrideApiDbContext _context;
        public GroupSettingsService(IGroupSettingsRepository groupSettingsRepository,
            ILogger<GroupSettingsService> logger)
        {
            _groupSettingsRepository= groupSettingsRepository;
            _logger= logger;
        }

        public CustomResponse<string> UpdateGroupName(int groupId, int userId, string newGroupName)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            Group group = new();
            string groupBackgroundCoverImage = null;
            try
            {
                bool isIdInteger = groupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _groupSettingsRepository.GetUserGroupRole(groupId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);

                else if (userRole == 0 || userRole == 1)
                {
                    group = _groupSettingsRepository.GetGroupById(groupId);
                    if (group != null)
                    {
                        group.name = newGroupName;
                        group.updated_date = DateTime.Now;
                        _groupSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateGroupBackgroundCoverPhoto);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeGroupName);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (group != null)
                {
                    groupBackgroundCoverImage = group.image_path;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheGroupsCurrentBackgroundCoverIsEmpty);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SETTINGS SERVICE UpdateBackgroundCoverImage METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<string>.Success(groupBackgroundCoverImage, true);
        }

        public CustomResponse<string> UpdateBackgroundCoverImage(int groupId, int userId, string path)
        {
            // Gelen grup id de admin rolündeki kullanıcıları çek
            // token içerisindeki userId al bu user grupta admin rolüne sahip mi ? 
            // Sahipse Güncelleme yapabilsin ve güncel image',in path ini dön
            // değilse hata kodu dön

            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            Group group = new();
            string groupBackgroundCoverImage = null;
            try
            {
                bool isIdInteger = groupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _groupSettingsRepository.GetUserGroupRole(groupId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);

                else if(userRole == 0 || userRole == 1)
                {
                    group = _groupSettingsRepository.GetGroupById(groupId);
                    if (group != null)
                    {
                        group.backgroundCover_path = path; 
                        group.updated_date = DateTime.Now;
                        _groupSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateGroupBackgroundCoverPhoto);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeBackgroundCoverGroup);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (group != null)
                {
                    groupBackgroundCoverImage = group.image_path;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheGroupsCurrentBackgroundCoverIsEmpty);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(" GROUP SETTINGS SERVICE UpdateBackgroundCoverImage METHOD ERROR  " + ex.Message);            
            }
            return CustomResponse<string>.Success(groupBackgroundCoverImage, true);
        }

        public CustomResponse<string> UpdateProfileImage(int groupId, int userId, string path)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            Group group = new();
            string groupProfileImage = null;
            try
            {
                bool isIdInteger = groupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<string>.Fail(errors, false);
                }

                var userRole = _groupSettingsRepository.GetUserGroupRole(groupId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);

                else if (userRole == 0 || userRole == 1)
                {
                    group = _groupSettingsRepository.GetGroupById(groupId);
                    if (group != null)
                    {
                        group.image_path = path;
                        group.updated_date = DateTime.Now;
                        _groupSettingsRepository.Update();
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.FailedToUpdateGroupProfilePhoto);
                        return CustomResponse<string>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToChangeProfilePhotoGroup);
                    return CustomResponse<string>.Fail(errors, false);
                }
                if (group != null)
                {
                    groupProfileImage = group.image_path;
                }
                else
                {
                    errors.Add(ErrorEnumResponse.TheGroupNameIsNull);
                    return CustomResponse<string>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SETTINGS SERVICE UpdateProfileImage METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<string>.Success(groupProfileImage, true);
        }

        public CustomResponse<string> AddUser(int groupId, int userId)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        // 30 MAYIS
        public CustomResponse<List<GroupMember>> DeleteUser(int groupId, int userId, List<int> memberId)
        {
            List<ErrorEnumResponse> errors = new List<ErrorEnumResponse>();
            List<int> memberIdList = new List<int>();
            List <GroupMember> updateGroupMembers = new List<GroupMember>();
            List <GroupMember> newGroupMember = new List<GroupMember>();
            try
            {
                // memberId group member tablsunda arayacağım
                // eğer varsa o kullanıcıyı grup_member tablosund adeaktife düşürmeliyim
                bool isGroupIdInteger = groupId.GetType() == typeof(int);
                bool isMemberIdInteger = groupId.GetType() == typeof(int);
                if (!isGroupIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<List<GroupMember>>.Fail(errors, false);
                }
                if (!isMemberIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupMemberIdIsNotInt);
                    return CustomResponse<List<GroupMember>>.Fail(errors, false);
                }

                var userRole = _groupSettingsRepository.GetUserGroupRole(groupId, userId);
                if (userRole < 0)
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);

                else if (userRole == 0 || userRole == 1)
                {
                    updateGroupMembers = _groupSettingsRepository.GetGroupMember(groupId, memberId);
                    if(updateGroupMembers == null)
                    {
                        errors.Add(ErrorEnumResponse.GroupMemberIsNull);
                        return CustomResponse<List<GroupMember>>.Fail(errors, false);
                    }
                    foreach (var groupMember in updateGroupMembers)
                    {
                        groupMember.active = 0;
                    }
                    _groupSettingsRepository.SaveChanges();

                    newGroupMember = _groupSettingsRepository.GetNewGroupMember(groupId);
                    if(newGroupMember == null)
                    {
                        errors.Add(ErrorEnumResponse.GroupMemberIsNull);
                        return CustomResponse<List<GroupMember>>.Fail(errors, false);
                    }
                }
                else
                {
                    errors.Add(ErrorEnumResponse.UserHasNoAuthorityToDeleteUserName);
                    return CustomResponse<List<GroupMember>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SETTINGS SERVICE DeleteUser METHOD ERROR  " + ex.Message);
            }
            return CustomResponse<List<GroupMember>>.Success(newGroupMember, true);
        }

    }
}
