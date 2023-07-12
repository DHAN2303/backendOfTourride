using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using AllrideApiCore.Dtos.ResponseDto.RoutePlannerResponseDto;
using AllrideApiCore.Entities.RoutePlanners;
using AllrideApiRepository.Repositories.Abstract.GroupsClubs;
using AllrideApiRepository.Repositories.Abstract.RoutePlannerRepo;
using AllrideApiService.Configuration.Extensions;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Routes;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace AllrideApiService.Services.Concrete.Routes
{
    public class RoutePlannerService : IRoutePlannerService
    {
        private readonly ILogger<RoutePlannerService> _logger;
        private readonly IRoutePlannerRepository _routePlannerRepository;
        private readonly IGroupRepository _groupRepos;
        private readonly IMapper _mapper;
        public RoutePlannerService(IMapper mapper, ILogger<RoutePlannerService> logger, IRoutePlannerRepository routePlannerRepository, IGroupRepository groupRepository)
        {
            _routePlannerRepository = routePlannerRepository;
            _logger = logger;
            _mapper = mapper;
            _groupRepos = groupRepository;
        }
        public CustomResponse<NoContentDto> SaveRoutePlanner(CreateRoutePlannerDto routePlanner, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (routePlanner == null)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerIsNull);
                    return CustomResponse<NoContentDto>.Fail(errors);
                }

                try
                {
                    if (!TimeSpan.TryParseExact(routePlanner.AlertTime, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan AlertTime))
                    {
                        errors.Add(ErrorEnumResponse.FailedToConvertAlertTimeDataTimespan);
                        return CustomResponse<NoContentDto>.Fail(errors, false);
                    }
                }
                catch (FormatException)
                {
                    _logger.LogError(" Timespan Format Is Not Correct In RoutePlannerService ");
                }

                var validator = new CreateRoutePlannerValidation();
                var response = validator.Validate(routePlanner).ThrowIfException();
                if (response.Status == false)
                {
                    return response;
                }

                // Mapleme yapılacak
                // RoutePlanner routePlannerModel = new();
                var routePlannerModel = _mapper.Map<RoutePlanner>(routePlanner);
                if (routePlannerModel == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // Öncelikle gelen kullanıcının UserId si veritabanında var mı veya bu kullanıcı aktif mi diye kontrol etmeliyim

                routePlannerModel.UserId = _routePlannerRepository.UserGetById(UserId);

                if (routePlannerModel.UserId <= 0)
                {
                    errors.Add(ErrorEnumResponse.UserIdNotFound);
                }

                var result = _routePlannerRepository.AddRoutePlanner(routePlannerModel);
                if (result == false)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerDoesntCreated);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                _routePlannerRepository.SaveChanges();

                return CustomResponse<NoContentDto>.Success(true);


            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service SaveRoutePlanner Log Error: " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }
        }
        public CustomResponse<NoContentDto> SaveRoutePlannerForGroup(CreateRoutePlannerForGroupDto routePlannerForGroup, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (routePlannerForGroup == null)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerIsNull);
                    return CustomResponse<NoContentDto>.Fail(errors);
                }

                try
                {
                    if (!TimeSpan.TryParseExact(routePlannerForGroup.AlertTime, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan AlertTime))
                    {
                        errors.Add(ErrorEnumResponse.FailedToConvertAlertTimeDataTimespan);
                        return CustomResponse<NoContentDto>.Fail(errors, false);
                    }
                }
                catch (FormatException)
                {
                    _logger.LogError(" Timespan Format Is Not Correct In RoutePlannerService ");
                }

                var validator = new CreateRoutePlannerForGroupDtoValidation();
                var response = validator.Validate(routePlannerForGroup).ThrowIfException();
                if (response.Status == false)
                {
                    return response;
                }

                // Gelen Id ye göre arama yapacak 
                // Mesela Grup dan geldiyse rota planlama grubunnId sini rota planlamaya kayıt edicek
                if (routePlannerForGroup.GroupId <= 0)
                {
                    errors.Add(ErrorEnumResponse.GroupIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // GroupId yi ara
                var isExistGroup = _routePlannerRepository.IsExistGroup(routePlannerForGroup.GroupId);

                if (isExistGroup == false)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerIsNull);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // Gelen UserId GroupMember tablosunda var mı ve bu User admin mi
                var groupsMember = _groupRepos.GetGroupMember(routePlannerForGroup.GroupId);

                if (groupsMember.role == 2 || groupsMember.role != 0 || groupsMember.role != 1)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerIsNull);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }


                // Kullanıcının rota planını eklemeye izni varsa
                // Mapleme yapılacak
                //RoutePlanner routePlannerModel = new();
                var routePlannerModel = _mapper.Map<RoutePlanner>(routePlannerForGroup);
                if (routePlannerModel == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // Öncelikle gelen kullanıcının UserId si veritabanında var mı veya bu kullanıcı aktif mi diye kontrol etmeliyim

                routePlannerModel.UserId = _routePlannerRepository.UserGetById(UserId);

                if (routePlannerModel.UserId <= 0)
                {
                    errors.Add(ErrorEnumResponse.UserIdNotFound);
                }

                var result = _routePlannerRepository.AddRoutePlanner(routePlannerModel);
                if (result == false)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerDoesntCreated);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                _routePlannerRepository.SaveChanges();

                return CustomResponse<NoContentDto>.Success(true);

            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service SaveRoutePlanner Log Error: " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }
        }
        // Add Friends In RoutePlanner
        public CustomResponse<List<int>> AddFriendsRoutePlanner(AddFriendsRoutePlannerDto addFriendsRoutePlanner, int UserId)
        {
            // Rotaya arkadaş ekleme sınırı var mı
            List<ErrorEnumResponse> errors = new();
            List<int> UserFailedToAddToRoutePlanning = new();
            try
            {
                if (addFriendsRoutePlanner.RoutePlannerId <= 0)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }
                // Önce Route Planner veritabanında var mı kontrol et
                RoutePlanner isRoutePlanner = _routePlannerRepository.GetById(addFriendsRoutePlanner.RoutePlannerId);
                if (isRoutePlanner == null)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerNotRegisterDB);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                // varsa addFriendsRoutePlanner daki usersları al hepsini users tablosunda ara burada olan kullanıcıları socialmedia followId de kur
                var IsExistUsersInDB = _routePlannerRepository.IsExistUsersInUserTable(addFriendsRoutePlanner.AddedUsers);
                if (IsExistUsersInDB == null)
                {
                    errors.Add(ErrorEnumResponse.UsersIsNotFoundUserTable);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                // SocialMedia Tablosunda ekli olan takipçiler, User tablosundaki ekli kullanıcılar aynı ise bunların Id listesini dönecek
                var IsExistUsersInSocialMediaFollowDb = _routePlannerRepository.IsExistFollowingIdInSocialMediaFollowTable(addFriendsRoutePlanner.AddedUsers);

                if (IsExistUsersInSocialMediaFollowDb == null || !IsExistUsersInSocialMediaFollowDb.Any())
                {
                    errors.Add(ErrorEnumResponse.UsersIsNotFoundSocialMediaFollowTable);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                // RotaPlanlamaya kullanıcı atarken UsersInRoutePlanning
                // Tablosunda daha önceden eklenen kullanıcı mı siye kotntrol etme
                // Veritabanında RotaPlanlaması = 1 olan kullanıcıları getiren kodu yaz
                //Is there a user already added to the route?

                var usersInTheRoutePlanningTable = _routePlannerRepository.SearchAddedUsers(addFriendsRoutePlanner.RoutePlannerId);
                List<int> NewAddedUsersInRoutePlanner = new();

                if (usersInTheRoutePlanningTable.Count <= 0)
                {
                    foreach (var addFriends in addFriendsRoutePlanner.AddedUsers)
                    {
                        NewAddedUsersInRoutePlanner.Add(addFriends);
                    }
                }
                else
                {
                    foreach (var af in addFriendsRoutePlanner.AddedUsers)
                    {
                        foreach (var userInTheRP in usersInTheRoutePlanningTable)
                        {
                            if (af == userInTheRP)
                                break;
                            else
                                NewAddedUsersInRoutePlanner.Add(af);
                        }
                    }
                }

                if (NewAddedUsersInRoutePlanner.Count <= 0)
                {
                    errors.Add(ErrorEnumResponse.InboundUsersAlreadyAddedToTheRoute);
                    return CustomResponse<List<int>>.Fail(NewAddedUsersInRoutePlanner, errors, false);
                }

                // Boş değilse bunu  UsersInRoutePlanning bu tabloya ekle

                UsersInRoutePlanning UsersInRoutePlanning = new();  // Tabloya eklemek için bir nesne örneği oluşturdum
                bool IsAddedUsersInRoutePlanner = false;
                bool IsUserAlreadyAddedInRoutePlanner = false;

                foreach (var AddedUsers in NewAddedUsersInRoutePlanner)
                {

                    IsUserAlreadyAddedInRoutePlanner = _routePlannerRepository.IsUserAlreadyAddedInRoutePlanner(isRoutePlanner.Id, AddedUsers);
                    if (IsUserAlreadyAddedInRoutePlanner)
                    {
                        UserFailedToAddToRoutePlanning.Add(UsersInRoutePlanning.Id);
                        errors.Add(ErrorEnumResponse.ThisUserHasAlreadyBeenAddedToThisRoute);
                        return CustomResponse<List<int>>.Fail(UserFailedToAddToRoutePlanning, errors, false);
                    }

                    // Şuanlık el ile mapleme yapacağım
                    UsersInRoutePlanning.RoutePlannerId = isRoutePlanner.Id;
                    UsersInRoutePlanning.SocialMediaFollower = AddedUsers;

                    if (UsersInRoutePlanning == null && UsersInRoutePlanning.TasksId.Any())
                    {
                        errors.Add(ErrorEnumResponse.MappingFailed);
                        return CustomResponse<List<int>>.Fail(errors, false);
                    }


                    IsAddedUsersInRoutePlanner = _routePlannerRepository.AddUsersInRoutePlanner(UsersInRoutePlanning);
                    if (IsAddedUsersInRoutePlanner == false)
                    {
                        UserFailedToAddToRoutePlanning.Add(UsersInRoutePlanning.Id);
                        errors.Add(ErrorEnumResponse.UserFailedToAddToRoutePlanning);
                        return CustomResponse<List<int>>.Fail(UserFailedToAddToRoutePlanning, errors, false);
                    }
                }

                return CustomResponse<List<int>>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service AddFriendsRoutePlanner Log Error: " + ex.Message);
                return CustomResponse<List<int>>.Fail(errors, false);
            }
        }

        // Rotadaki kullanıcılara atanan görevleri kayıt eder
        public CustomResponse<List<int>> AssigningTasksToUsersOnARoute(AddFriendsTasksRoutePlannerDto addFriendsTasksRoutePlanner, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (addFriendsTasksRoutePlanner.RoutePlannerId <= 0)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                if (addFriendsTasksRoutePlanner == null)
                {
                    errors.Add(ErrorEnumResponse.AddFriendsTasksRoutePlannerDtoIsNull);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                foreach (var key in addFriendsTasksRoutePlanner.FriendsAndTasksId.Keys)
                {
                    if (key is int)
                    {
                        continue;
                    }
                    else
                    {
                        errors.Add(ErrorEnumResponse.UserIdIsNotInteger);
                        return CustomResponse<List<int>>.Fail(errors, false);
                    }
                }

                // Validasyonlar
                var validator = new AssigningATaskUserInRoutePlanningValidation();
                var response = validator.Validate(addFriendsTasksRoutePlanner).ThrowIfException();
                if (response.Status == false)
                {
                    foreach (var error in response.ErrorEnums)
                    {
                        errors.Add(error);
                    }
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                RoutePlanner isRoutePlanner = _routePlannerRepository.GetById(addFriendsTasksRoutePlanner.RoutePlannerId);
                if (isRoutePlanner == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                var friendsIdList = addFriendsTasksRoutePlanner.FriendsAndTasksId.Keys.ToList();

                var IsExistUsersInDB = _routePlannerRepository.IsExistUsersInUserTable(friendsIdList);

                if (IsExistUsersInDB == null)
                {
                    errors.Add(ErrorEnumResponse.UsersIsNotFoundUserTable);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                var UsersInRoutePlanningList = _routePlannerRepository.GetUserInRoutePlanning(addFriendsTasksRoutePlanner.RoutePlannerId);

                if (UsersInRoutePlanningList.Count <= 0)
                {
                    errors.Add(ErrorEnumResponse.FriendsNotAddedInTableForTasks);
                    return CustomResponse<List<int>>.Fail(errors, false);
                }

                List<int> followers = new();
                foreach (var user in UsersInRoutePlanningList)
                {
                    followers.Add(user.SocialMediaFollower);
                }

                // Rota Planına Eklenmeyen kullanıcıların listesi
                List<int> missingUsers = friendsIdList.Except(followers).ToList();

                // Rota planında olan kullanıcıların görevlerini ekledikten sonra eklenmeyen varsa false dön
                if (missingUsers.Count > 0)
                {
                    errors.Add(ErrorEnumResponse.UsersInListNotAddedToRoutePlanning);
                    return CustomResponse<List<int>>.Fail(missingUsers, errors, false);
                }

                List<int> count = new();
                // Kullanıcılara Task Atama
                foreach (var UsersInRoutePlanning in UsersInRoutePlanningList)
                {
                    if (addFriendsTasksRoutePlanner.FriendsAndTasksId != null)
                    {
                        foreach (var usersTasks in addFriendsTasksRoutePlanner.FriendsAndTasksId)
                        {
                            if (UsersInRoutePlanning.SocialMediaFollower == usersTasks.Key)
                            {
                                if (usersTasks.Value.Any())
                                {
                                    var result = _routePlannerRepository.AssigningTasksToUsersOnARoute(usersTasks.Value, UsersInRoutePlanning.Id);

                                    if (result == false)
                                    {
                                        count.Add(usersTasks.Key);
                                        errors.Add(ErrorEnumResponse.FailedToAssignTaskToUserAndCount);
                                    }
                                    // break;

                                }

                            }

                        }
                    }
                }

                if (count.Count <= 0)
                    return CustomResponse<List<int>>.Success(true);
                else
                    return CustomResponse<List<int>>.Fail(count, errors, false);


            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service AddFriendsRoutePlanner Log Error: " + ex.Message);
                return CustomResponse<List<int>>.Fail(errors, false);
            }
        }
        // Burada hata çıkarsa canını sıkma, seni seviyorum :*
        public CustomResponse<NoContentDto> DeleteRoutePlanner(int RoutePlannerId, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (RoutePlannerId < 0)
                {
                    errors.Add(ErrorEnumResponse.RouteIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // RoutePlanner Id varsa veritabanından silicez

                var IsExist = _routePlannerRepository.IsExistId(RoutePlannerId);

                if (IsExist == false)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerNotRegisterDB);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // Yoksa  

                var isDeleted = _routePlannerRepository.Delete(RoutePlannerId);

                if (isDeleted == false)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerDoesntDeleted);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                _routePlannerRepository.SaveChanges();

                return CustomResponse<NoContentDto>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service SaveRoutePlanner Log Error:  " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }

        }
        public CustomResponse<NoContentDto> AddTasks(CreateTasksInRoutePlanner createTasks, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (createTasks.RoutePlannerId < 0)
                {
                    errors.Add(ErrorEnumResponse.RouteIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }


                var validator = new CreateTasksInRoutePlannerValidation();
                var response = validator.Validate(createTasks).ThrowIfException();
                if (response.Status == false)
                {
                    foreach (var error in response.ErrorEnums)
                    {
                        errors.Add(error);
                    }
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }
                // Mapleme
                var Tasks = _mapper.Map<TasksRoutePlanner>(createTasks);
                if (Tasks == null)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                var getRoutePlanner = _routePlannerRepository.IsExistId(createTasks.RoutePlannerId);
                if (getRoutePlanner == false)
                {
                    errors.Add(ErrorEnumResponse.RouteDetailDoesntRegisterDb);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                var isAdd = _routePlannerRepository.AddTasks(Tasks);
                if (isAdd == false)
                {
                    errors.Add(ErrorEnumResponse.TasksDontAdded);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                return CustomResponse<NoContentDto>.Success(true);

            }
            catch (Exception ex)
            {
                _logger.LogError("Route Planner Service SaveRoutePlanner Log Error:  " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors, false);
            }

        }
        public CustomResponse<NoContentDto> UserLeaving(int RoutePlannerId, int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                if (RoutePlannerId<0)
                {
                    errors.Add(ErrorEnumResponse.RouteIdCannotBeEqualTo0AndLessThanZero);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }
                // Gelen RoutePlannerID veritabanında mevcut mu
                var getRoutePlannerId = _routePlannerRepository.GetById(RoutePlannerId);
                if (getRoutePlannerId == null)
                {
                    errors.Add(ErrorEnumResponse.RoutePlannerNotRegisterDB);
                    return CustomResponse<NoContentDto>.Fail(errors, false);
                }

                // Mevcutsa ayrılmak istenen kullanıcı bu rota planlamada var mı kontrol et
                var isExistUserInRoutePlanner = _routePlannerRepository.LeaveUserInRoutePlanner(RoutePlannerId, UserId);
                if(isExistUserInRoutePlanner == false)
                {
                    errors.Add(ErrorEnumResponse.UserNotRegisteredInRoutePlan);
                    return CustomResponse<NoContentDto>.Fail(errors);
                }

                _routePlannerRepository.SaveChanges();

                return CustomResponse<NoContentDto>.Success(true);

            }
            catch(Exception ex)
            {
                _logger.LogError(" ROUTE PLANNER SERVICE USERLEAVING METHOD ERROR: " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors);
            }
        }
        public CustomResponse<List<UserHaveRoutePlannerResponseDto>> GetAllRoutePlanner(int UserId)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                // User Id nin Rota Planlaması var mı
                var checkUserRoutePlanner = _routePlannerRepository.CheckUserRoutePlanner(UserId);
                if (checkUserRoutePlanner == false)
                {
                    errors.Add(ErrorEnumResponse.UserDoesntHaveRoutePlanner);
                    return CustomResponse<List<UserHaveRoutePlannerResponseDto>>.Fail(errors, false);
                }

                var userHaveRoutePlannerList = _routePlannerRepository.UserGetRoutePlanner(UserId);

                if (userHaveRoutePlannerList.Any() == false)
                {
                    errors.Add(ErrorEnumResponse.UserDoesntHaveRoutePlanner);
                    return CustomResponse<List<UserHaveRoutePlannerResponseDto>>.Fail(errors, false);
                }

                // Mapleme yapılcak
                List<UserHaveRoutePlannerResponseDto> userHaveRoutePlannerResponseDtos = new();

                foreach (var userHaveRoutePlanner in userHaveRoutePlannerList)
                {
                    UserHaveRoutePlannerResponseDto userHaveRoutePlannerResponseDto = new(); //_mapper.Map<UserHaveRoutePlannerResponseDto>(userHaveRoutePlanner);
                    userHaveRoutePlannerResponseDto.RouteId = userHaveRoutePlanner.RouteId;
                    userHaveRoutePlannerResponseDto.RoutePlannerTitle = userHaveRoutePlanner.RoutePlannerTitle;
                    userHaveRoutePlannerResponseDto.RouteName = userHaveRoutePlanner.RouteName;
                    userHaveRoutePlannerResponseDto.StartDate = userHaveRoutePlanner.StartDate;
                    userHaveRoutePlannerResponseDto.ColorCodeHex = userHaveRoutePlanner.ColorCodeHex;
                    userHaveRoutePlannerResponseDto.RouteAlertTime = userHaveRoutePlanner.RouteAlertTime;
                    userHaveRoutePlanner.UsersInRoutePlannings = userHaveRoutePlanner.UsersInRoutePlannings.ToList();
                  

                    if (userHaveRoutePlannerResponseDto == null)
                    {
                        errors.Add(ErrorEnumResponse.MappingFailed);
                    }

                    userHaveRoutePlannerResponseDtos.Add(userHaveRoutePlannerResponseDto);
                }

                return CustomResponse<List<UserHaveRoutePlannerResponseDto>>.Success(userHaveRoutePlannerResponseDtos, true);

            }
            catch(Exception ex)
            {
                _logger.LogError(" ROUTE PLANNER SERVICE GetAllRoutePlanner METHOD ERROR: " + ex.Message);
                return CustomResponse<List<UserHaveRoutePlannerResponseDto>>.Fail(errors);
            }
        }
        public CustomResponse<NoContentDto> DeleteTaskFromRoutePlanner(DeleteTaskRoutePlannerDto deleteTaskRoutePlannerDto)
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                // Validasyonlar yapıldı
                var validator = new DeleteTaskRoutePlannerValidation();
                var response = validator.Validate(deleteTaskRoutePlannerDto).ThrowIfException();
                if (response.Status == false)
                {
                    return response;
                }

                //  Gelen Task tablosunda gelen route planner id ye ait kayıt var mı varsa bu kaydı sil

                var IstheTaskIncludedInTheRoutePlanner = _routePlannerRepository.IsExistTaskFromRoutePlanner(deleteTaskRoutePlannerDto);
                if(IstheTaskIncludedInTheRoutePlanner== false)
                {
                    errors.Add(ErrorEnumResponse.TaskIsNotAttachedToTheRoutePlan);
                    return CustomResponse<NoContentDto>.Fail(errors,false);
                }

                var IsDeletedTaskInRoutePlanner = _routePlannerRepository.DeleteTaskInRoutePlanner(deleteTaskRoutePlannerDto);
                if(IsDeletedTaskInRoutePlanner== false)
                {
                    errors.Add(ErrorEnumResponse.TaskIsNotDeletedInTable);
                    return CustomResponse<NoContentDto>.Fail(errors,false);
                }

                _routePlannerRepository.SaveChanges();

                return CustomResponse<NoContentDto>.Success(true);

            }
            catch (Exception ex)
            {
                _logger.LogError(" ROUTE PLANNER SERVICE GetAllRoutePlanner METHOD ERROR: " + ex.Message);
                return CustomResponse<NoContentDto>.Fail(errors);
            }
        }

    }
}
//public CustomResponse<NoContentDto> DeleteUserForGrupOrClubRoutePlanner(DeleteFriendsRoutePlanner deleteFriendsRoutePlanner, int userId)
//{
//    List<ErrorEnumResponse> errors = new();
//    try
//    {
//        if (deleteFriendsRoutePlanner == null)
//        {
//            errors.Add(ErrorEnumResponse.RoutePlannerIsNull);
//            return CustomResponse<NoContentDto>.Fail(errors, false);
//        }

//        // Dto nun Validasyonunu yap 
//        var validator = new DeleteFriendsRoutePlannerValidation();
//        var validateDto = validator.Validate(deleteFriendsRoutePlanner).ThrowIfException();
//        if (validateDto.Status == false)
//        {
//            return validateDto;
//        }

//        // Gelen RoutePlannerID veritabanında mevcut mu
//        var getRoutePlannerId = _routePlannerRepository.GetById(deleteFriendsRoutePlanner.RoutePlannerId);
//        if(getRoutePlannerId == null)
//        {
//            errors.Add(ErrorEnumResponse.RoutePlannerNotRegisterDB);
//            return CustomResponse<NoContentDto>.Fail(errors, false);
//        }

//        // Silme isteği yapan kullanıcının silmeye yetkisi var mı
//        // User oluşturmuşsa sadece rota planını oluşturan kullanıcı silebilir.

//    }
//    catch (Exception ex)
//    {

//    }
//}

// else
//{
//    foreach (var friendId in addFriendsTasksRoutePlanner.FriendsAndTasksId.Keys)
//    {
//        foreach (var usersInRoute in UsersInRoutePlanningList)
//        {
//            if (friendId == usersInRoute.SocialMediaFollower) // Eğer kullanıcı rotaya daha önceden eklenmişse sadece görevini ata
//            {
//                if (usersInRoute.TasksId == null)
//                {
//                    usersInRoute.TasksId.Add(addFriendsTasksRoutePlanner.FriendsAndTasksId[friendId]);
//                }
//            }
//            else
//            {

//            }

//            result = _routePlannerRepository.AssigningTasksToUsersOnARoute(usersInRoute);

//            if (result == false)
//            {
//                count++;
//                errors.Add(ErrorEnumResponse.FailedToAssignTaskToUserAndCount);
//                return CustomResponse<int>.Fail(count, errors, false);
//            }
//        }
//        _routePlannerRepository.SaveChanges();
//    }

//}

//foreach (var friendIdTasks in addFriendsTasksRoutePlanner.FriendsAndTasksId.Keys)
//{

//    UsersInRoutePlanning usersInRoutePlanning = new UsersInRoutePlanning();
//    usersInRoutePlanning.RoutePlannerId = addFriendsTasksRoutePlanner.RoutePlannerId;
//    usersInRoutePlanning.SocialMediaFollower = friendIdTasks;
//    usersInRoutePlannings.Add(usersInRoutePlanning);
//    foreach (var taskId in usersInRoutePlanning.TasksId)
//    {
//        usersInRoutePlanning.TasksId.Add(taskId);
//    }
//    result = _routePlannerRepository.AssigningTasksToUsersOnARoute(usersInRoutePlanning);

//    if (result == false)
//    {
//        count++;
//        errors.Add(ErrorEnumResponse.FailedToAssignTaskToUserAndCount);
//        return CustomResponse<int>.Fail(count, errors, false);
//    }
//    _routePlannerRepository.SaveChanges();
//}



// Sosyal Medya  tablosundaki takipçilerinde varsa listeye ekle
//List<int> FollowersAddedToRoutePlan = new();
//foreach (int UserList in IsExistUsersInDB)
//{
//    foreach (int FollowingList in IsExistUsersInSocialMediaFollowDb)
//    {
//        if (UserList == FollowingList)
//        {
//            FollowersAddedToRoutePlan.Add(FollowingList);
//        }
//        else
//        {
//            break;
//        }
//    }
//}



//List<int> AddedUsersInRoutePlannerList = new();  // Rota planlamaya ekli kullanıcılar
//var userAlreadyAdded = _routePlannerRepository.SearchAddedUsers(addFriendsRoutePlanner.RoutePlannerId); // Aynı rotaya daha önceden ekli kullanıcı var mı

//// Requestten gelen kullanıcıların içerisinde gez 
//foreach (var addFriends in addFriendsRoutePlanner.AddedUsers)
//{
//    // Eğer gelen rota da daha önce kayıtlı kullanıcı yoksa
//    if (userAlreadyAdded.Count <= 0)
//    {
//        AddedUsersInRoutePlannerList.Add(addFriends); // Rota planına eklencek kullanıcıları tüm listeye ekliyor
//    }
//    else
//    {
//        // Requestten gelen kullanıcı, daha önceden rota planına eklenmişse bu kullanıcıyı atla
//        foreach (var addedUser in userAlreadyAdded)
//        {
//            if (addFriends == addedUser)
//            {
//                break;
//            }
//            else
//            {
//                AddedUsersInRoutePlannerList.Add(addedUser);
//            }
//        }
//    }
//}


//AddPlanedRouteUsers addPlanedUsers = new();
//var result = _routePlannerRepository.AddFriendsRoutePlanner(addPlanedUsers, UserId);
//if (result == false)
//{
//    errors.Add(ErrorEnumResponse.FriendsDoesntAddedInRoutePlanner);
//    return CustomResponse<NoContentDto>.Fail(errors, false);
//}

//List<UsersInRoutePlanning> usersInRoutePlannings = new();
//foreach (var User in UsersAttachedToTheRoutePlan)
//{
//    foreach (var usersInRoutePlanning in usersInRoutePlannings)
//    {
//        usersInRoutePlanning = _mapper.Map<UsersInRoutePlanning>(routePlanner);
//    }
//    if (usersInRoutePlanning == null)
//        _routePlannerRepository.AddUsersInRoutePlanner(usersInRoutePlanning);
//}