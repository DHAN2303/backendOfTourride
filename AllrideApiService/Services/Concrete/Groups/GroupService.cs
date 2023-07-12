using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AllrideApiService.Services.Concrete.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GroupService> _logger;
        public GroupService(IGroupRepository groupRepository, ILogger<GroupService> logger)
        {
            _groupRepository = groupRepository;
            _logger = logger;
        }
        public CustomResponse<UserResponseDto> GetGroupUserDetail(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            UserResponseDto userDetail = null;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<UserResponseDto>.Fail(errors, false);
                }
                if (_groupRepository.IsExistGroup(GroupId))
                {
                    userDetail = _groupRepository.GetGroupUserDetail(GroupId);
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
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<UserResponseDto>.Success(userDetail, true);
        }
        public CustomResponse<GlobalGroupResponseDto> DeleteUserInGroup(int GroupId, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
                if (UserId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }

                if (_groupRepository.IsExistUserInGroup(GroupId, UserId))
                {
                    bool isGroupDeleted = _groupRepository.DeleteUserInGroup(GroupId, UserId);
                    return CustomResponse<GlobalGroupResponseDto>.Success("GroupID:" + GroupId + " UserId:" + UserId, true);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE DeleteUserInGroup METHOD LOG ERROR " + ex.Message);
                return CustomResponse<GlobalGroupResponseDto>.Success("GroupID:" + GroupId + " UserId:" + UserId, false);

            }


        }
        public CustomResponse<GlobalGroupResponseDto> DeleteGroup(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
                if (_groupRepository.IsExistGroup(GroupId))
                {
                    bool isGroupDeleted = _groupRepository.DeleteGroup(GroupId);
                    return CustomResponse<GlobalGroupResponseDto>.Success("" + GroupId, true);
                }
                else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE DeleteGroup METHOD LOG ERROR " + ex.Message);
                return CustomResponse<GlobalGroupResponseDto>.Success("" + GroupId, false);

            }

        }
        public CustomResponse<GlobalGroupResponseDto> GetGlobalGroups(int GroupId, int Type)
        {
            List<ErrorEnumResponse> errors = new();
            List<GlobalGroupResponseDto> groupResponse = null; ;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }

                groupResponse = _groupRepository.GetGlobalGroups(GroupId, Type);
                if (groupResponse == null || groupResponse.Count < 1)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGlobalGroups METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<GlobalGroupResponseDto>.Success(groupResponse[0], true);
        }
        public CustomResponse<GroupResponseDto> GetGroupDetail(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            GroupResponseDto groupResponseDto = null;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }

                groupResponseDto = _groupRepository.GetGroupDetail(GroupId);
                if (groupResponseDto == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<GroupResponseDto>.Success(groupResponseDto, true);
        }
        public CustomResponse<GroupResponseDto> GetGroupMedia(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            List<GroupResponseDto> groupResponse = null;
            try
            {
                if (GroupId < 0 && GroupId is int == false)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }

                groupResponse = _groupRepository.GetMedia(GroupId);
                if (groupResponse == null || groupResponse.Count < 1)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GET GROUP MEDIA METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<GroupResponseDto>.Success(groupResponse[0], true);

        }
        public CustomResponse<List<string>> SearchUserGroup(string userName, int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            List<string> users = new();
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = GroupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }

                if (userName.IsNullOrEmpty())
                {
                    errors.Add(ErrorEnumResponse.UserIdCannotBeNullOrEmptyInClub);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }
                users = _groupRepository.SearchUserGroup(userName, GroupId);
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
        public CustomResponse<LastActivityResponseDto> GetLastActivity(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            LastActivityResponseDto lastActivity = new();
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = GroupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<LastActivityResponseDto>.Fail(errors, false);
                }
                lastActivity = _groupRepository.GetLastActivity(GroupId);
                if (lastActivity == null)
                {
                    errors.Add(ErrorEnumResponse.ActivityDontRegisterDB);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetLastActivity ERROR:  " + ex.Message);
            }
            return CustomResponse<LastActivityResponseDto>.Success(lastActivity, true);
        }
        // 29 Mayıs
        public CustomResponse<List<GroupResponseDto>> GetUsersGroupList(int userId)
        {
            List<ErrorEnumResponse> errors = new();
            List<GroupResponseDto> groupResponseDto = new();
            try
            {
                groupResponseDto = _groupRepository.GetUsersGroupList(userId);
                if (groupResponseDto == null)
                {
                    errors.Add(ErrorEnumResponse.UserIsNotInAGroup);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetLastActivity ERROR:  " + ex.Message);
            }
            return CustomResponse<List<GroupResponseDto>>.Success(groupResponseDto, true);
        }
        // 29 - 30 - 31 Mayıs
        public CustomResponse<List<GroupSocialMediaPostsResponseDto>> GetGroupsUsersSocialMediaLast3Post(int groupId)
        {
            List<ErrorEnumResponse> errors = new();
            List<GroupSocialMediaPostsResponseDto> groupSocialPostWithCommentList = new();
            try
            {
                if (groupId < 0 || groupId is int == false)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<List<GroupSocialMediaPostsResponseDto>>.Fail(errors, false);
                }

                groupSocialPostWithCommentList = _groupRepository.GetGroupsUsersSocialMediaLast3Post(groupId);
                if (groupSocialPostWithCommentList.Count < 0)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoPostSharedInTheClub);
                    return CustomResponse<List<GroupSocialMediaPostsResponseDto>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetClubsUsersSocialMediaLast3Post METHOD ERROR:  " + ex.Message);
            }
            return CustomResponse<List<GroupSocialMediaPostsResponseDto>>.Success(groupSocialPostWithCommentList, false);
        }
        public CustomResponse<string> UpdateBackgroundCoverAndProfilePhoto(int groupId)
        {
            List<ErrorEnumResponse> errors = new();
            string imagePath = null;
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = groupId.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<string>.Fail(errors, false);
                }
                imagePath = _groupRepository.UpdateProfileOrBacgroundImage(groupId);
                if (imagePath == null)
                {
                    errors.Add(ErrorEnumResponse.GroupHasNotImage);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE UpdateBackgroundCoverAndProfilePhoto ERROR:  " + ex.Message);
            }
            return CustomResponse<string>.Success(imagePath, true);
        }

        // 13 Haziran
        public CustomResponse<List<UserProfileResponseDto>> GetFollowers(int groupId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (groupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
                }

                var result = _groupRepository.GetGroupsUsers(groupId);
                if (result == null)
                {
                    errors.Add(ErrorEnumResponse.GroupMemberIsNull);
                    return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
                }

                return CustomResponse<List<UserProfileResponseDto>>.Success(result, true);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " GroupService -->  GetFollowers METHOD ERROR: " + ex.InnerException.ToString());
                return CustomResponse<List<UserProfileResponseDto>>.Fail(errors, false);
            }
        }
    }
}
