using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.ResponseDtos;
using AllrideApiCore.Entities.Chat;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.GroupsInfo;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SMSApi.Api.Action;

namespace AllrideApiService.Services.Concrete.Groups
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GroupService> _logger;
        public GroupService(IGroupRepository groupRepository, ILogger<GroupService> logger)
        {
            _groupRepository = groupRepository;
            _logger = logger;
        }
        public CustomResponse<Object> GetGroupUserDetail(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            List<UserResponseDto> userDetail = null;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<Object>.Fail(errors, false);
                }
                if (_groupRepository.IsExistGroup(GroupId))
                {
                    userDetail = _groupRepository.GetGroupUserDetail(GroupId);
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
        public CustomResponse<GlobalGroupResponseDto> DeleteUserInGroup(int GroupId, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
                if (UserId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }

                if (_groupRepository.IsExistUserInGroup(GroupId, UserId))
                {
                    bool isGroupDeleted = _groupRepository.DeleteUserInGroup(GroupId, UserId);
                    return CustomResponse<GlobalGroupResponseDto>.Success("GroupID:" + GroupId+ " UserId:"+ UserId , true);
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
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
                if (_groupRepository.IsExistGroup(GroupId))
                {
                    bool isGroupDeleted = _groupRepository.DeleteGroup(GroupId);
                    return CustomResponse<GlobalGroupResponseDto>.Success("" + GroupId, true);
                }else
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }
                  
               
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE DeleteGroup METHOD LOG ERROR " + ex.Message);
                return CustomResponse<GlobalGroupResponseDto>.Success(""+GroupId, false);
                
            }
            
        }

        public CustomResponse<GroupResponseDto> GetGroupDetail(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            GroupResponseDto groupResponseDto = null;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
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

        public CustomResponse<GlobalGroupResponseDto> GetGlobalGroups(int GroupId, int Type)
        {
            List<ErrorEnumResponse> errors = new();
            List<GlobalGroupResponseDto> groupResponse = null; ;
            try
            {
                if (GroupId < 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GlobalGroupResponseDto>.Fail(errors, false);
                }

                groupResponse = _groupRepository.GetGlobalGroups(GroupId, Type);
                if (groupResponse == null || groupResponse.Count<1)
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
        public CustomResponse<Object> GetMemberedGroupsByUser(int userId)
        {
            List<ErrorEnumResponse> errors = new();
            List<Group> groups =null;
            try
            {
                if (userId < 0)
                {
                    errors.Add(ErrorEnumResponse.NoUserIdInUserDetailTable);
                    return CustomResponse<Object>.Fail(errors, false);
                }

                groups = _groupRepository.GetGroupsForUser(userId);
                if (groups == null)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<Object>.Fail(errors, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GetGroupDetail METHOD LOG ERROR " + ex.Message);
            }

            return CustomResponse<Object>.Success(groups, true);
        }
        public CustomResponse<GroupResponseDto> GetGroupMedia(int GroupId)
        {
            List<ErrorEnumResponse> errors = new();
            List <GroupResponseDto> groupResponse = null;
            try
            {
                if (GroupId < 0 && GroupId is int == false)
                {
                    errors.Add(ErrorEnumResponse.GroupIdIsNotInt);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }

                groupResponse = _groupRepository.GetMedia(GroupId);
                if(groupResponse == null || groupResponse.Count < 1)
                {
                    errors.Add(ErrorEnumResponse.ThereIsNoSuchGroupInDb);
                    return CustomResponse<GroupResponseDto>.Fail(errors, false);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(" GROUP SERVICE GET GROUP MEDIA METHOD LOG ERROR "+ex.Message);
            }

            return CustomResponse<GroupResponseDto>.Success(groupResponse[0], true);

        }
        public CustomResponse<List<string>> SearchUserGroup (string userName, int GroupId) 
        {
            List<ErrorEnumResponse> errors = new();
            List<string> users = new();
            try
            {
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = GroupId.GetType() == typeof(int);
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
                users = _groupRepository.SearchUserGroup(userName, GroupId);
                if(users == null|| users.Count < 1)
                {
                    errors.Add(ErrorEnumResponse.SearchedUserNotFoundinGroup);
                    return CustomResponse<List<string>>.Fail(errors, false);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError (" GROUP SERVICE SearchGroup ERROR:  " + ex.Message);
            }

            return CustomResponse<List<string>>.Success(users, true);
        }

        public CustomResponse<Object> GetGroupMessage(int groupId)
        {
            var response = _groupRepository.GetGroupMessage(groupId);
            if (response != null)
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
