using AllrideApiCore.Dtos.RequestDto;
using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Routes;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Configuration.Extensions;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.Routes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Routes
{
    public class RouteServices : IRoutesServices
    {
        protected readonly AllrideApiDbContext _context;
        private readonly IRouteUserFetchRepository _userFetchRepository;
        private readonly IRoutingRepository _routingRepository;
        private readonly ILogger<RouteServices> _logger;
        private readonly IMapper _mapper;

        public RouteServices(AllrideApiDbContext context, IMapper mapper, IRoutingRepository routingRepository, IRouteUserFetchRepository userFetchRepository)
        {
            _context = context;
            _mapper = mapper;
            _routingRepository = routingRepository;
            _userFetchRepository = userFetchRepository;
        }
        public async Task<List<Route>> fetchMyRoutes(string user_id)
        {
            var myRoutes = await (
                from r in _context.route
                join umr in _context.user_my_route on r.Id equals umr.route_id
                where umr.user_id == Convert.ToInt32(user_id)
                select r
            ).ToListAsync();

            return myRoutes;
        }
        public async Task<List<Route>> fetchPublishedRoutes(string user_id)
        {
            var sharedRoutes = await (
                from r in _context.route
                join umr in _context.user_shared_route on r.Id equals umr.route_id
                where umr.user_id == Convert.ToInt32(user_id)
                select r
            ).ToListAsync();

            return sharedRoutes;
        }
        public async Task<List<Route>> fetchFavoriteRoutes(string user_id)
        {
            var favoriteRoutes = await (
                from r in _context.route
                join umr in _context.user_favorite_route on r.Id equals umr.route_id
                where umr.user_id == Convert.ToInt32(user_id)
                select r
            ).ToListAsync();

            return favoriteRoutes;
        }
        public CustomResponse<RouteDetailResponseDto> FetchUsersRoute(FetchUsersRouteRequestDto fetchUsersRouteDto)
        {
            // RouteId tablosudna 
            RouteDetailResponseDto _fetchUsersResponseDto = new();
            List<ErrorEnumResponse> _errors = new();
            try
            {
                var validator = new FetchUsersRouteConfiguration();
                var isValid = validator.Validate(fetchUsersRouteDto).ThrowIfException();

                if (isValid.Status == false) //isValid.ListErrorEnums.Count>0
                {
                    return CustomResponse<RouteDetailResponseDto>.Fail(isValid.ErrorEnums);
                }
                var routeDetail = _userFetchRepository.GetRouteDetail(fetchUsersRouteDto.RouteId);
                if (routeDetail == null)
                {
                    _errors.Add(ErrorEnumResponse.RouteDetailDoesntRegisterDb);
                    return CustomResponse<RouteDetailResponseDto>.Fail(_errors, false);
                }
                _fetchUsersResponseDto = _mapper.Map<RouteDetailResponseDto>(routeDetail);
                if (_fetchUsersResponseDto == null)
                {
                    _errors.Add(ErrorEnumResponse.BackendDidntAutoMapper);
                    return CustomResponse<RouteDetailResponseDto>.Fail(_errors, false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(" FETCH ROUTE DETAIL SERVICE " + e.Message, e);
            }


            return CustomResponse<RouteDetailResponseDto>.Success(_fetchUsersResponseDto, true);


        }
        public CustomResponse<List<RouteDetailResponseDto>> GetRecommendedRoute(int recommendedType)
        {
            // bana bir parametre göndermesine gerek var mı? 

            List<ErrorEnumResponse> _errors = new();
            List<RouteDetailResponseDto> _routeDetailsResponseDtoList = new();
            try
            {
                if (recommendedType is int == false || recommendedType < 0)
                {
                    _errors.Add(ErrorEnumResponse.RecommendedTypeIsNotInt);
                    return CustomResponse<List<RouteDetailResponseDto>>.Fail(_errors, false);
                }
                if (recommendedType < 0)
                {
                    _errors.Add(ErrorEnumResponse.RecommendedTypeLessThanZero);
                    return CustomResponse<List<RouteDetailResponseDto>>.Fail(_errors, false);
                }

                // Rota tablosunda rota kategorisini arayacağım editor_advice seçeneğini bakıcam
                var routes = _routingRepository.GetRecommendedRoute(recommendedType);
                if (routes == null)
                {
                    _errors.Add(ErrorEnumResponse.NoRecommendedRouteAdded);
                    return CustomResponse<List<RouteDetailResponseDto>>.Fail(_errors, false);
                }
                // aşağıda atamaişlemi yapamazda Add metodunu kullanacağım
                List<RouteDetail> routeDetails = _routingRepository.GetRecommendedRouteDetail(recommendedType);
                if (routeDetails == null || routeDetails.Count<0)
                {
                    _errors.Add(ErrorEnumResponse.NotGetRoutesDetailInDb);
                    return CustomResponse<List<RouteDetailResponseDto>>.Fail(_errors, false);
                }
                foreach (var item in routeDetails)
                {
                    RouteDetailResponseDto routeDetailResponseDto = _mapper.Map<RouteDetailResponseDto>(item);
                    if (item == null)
                        continue;
                    _routeDetailsResponseDtoList.Add(routeDetailResponseDto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(" ROUTE SERVİCE ERROR " + e.Message);
            }


            return CustomResponse<List<RouteDetailResponseDto>>.Success(_routeDetailsResponseDtoList, true);
        }

        public CustomResponse<List<Last3RoutesUserResponseDto>> GetUserLast3Routes(int UserId)
        {
            List<ErrorEnumResponse> _errors = new();
            List<Last3RoutesUserResponseDto> lastRouteResponseDtoList = new();
            try
            {
                var response = _routingRepository.GetLast3Routes(UserId);
                if(response == null)
                {
                    _errors.Add(ErrorEnumResponse.NoRouteSavedInDb);
                    return CustomResponse<List<Last3RoutesUserResponseDto>>.Fail(_errors, false);

                }
                foreach(var item in response)
                {
                    Last3RoutesUserResponseDto last3RoutesUserResponseDto = _mapper.Map<Last3RoutesUserResponseDto>(item);
                    if (item == null)
                        continue;
                    lastRouteResponseDtoList.Add(last3RoutesUserResponseDto);
                }
                if(lastRouteResponseDtoList == null)
                {
                    _errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<Last3RoutesUserResponseDto>>.Fail(_errors, false);
                }


            }
            catch (Exception e)
            {
                _logger.LogError(" ROUTE SERVİCE GetUserLast3Routes METHOD ERROR " + e.Message);
            }

            return CustomResponse<List<Last3RoutesUserResponseDto>>.Success(lastRouteResponseDtoList, true);
        }


    }
}
