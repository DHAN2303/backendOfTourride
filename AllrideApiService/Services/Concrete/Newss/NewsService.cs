using AllrideApiCore.Dtos;
using AllrideApiCore.Dtos.Here;
using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.Select;
using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Users;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Configuration.Extensions;
using AllrideApiService.Configuration.Validator;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.News;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace AllrideApiService.Services.Concrete.Newss
{
    public class NewsService : INewsService
    {
        private const string V = "News Service Katmanı PostNews Metodu Error Log Kaydı: ";
        private const string V1 = " USER NEWS REACTION DID'NT REGISTER:  ";
        private const string V2 = " News Service Log Error: ";
        private readonly INewsRepository _newsRepository;
        private readonly IUserNewsReactionRepository _userNewsReactionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NewsService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NewsService(INewsRepository newsRepository,
            IMapper mapper, ILogger<NewsService> logger,
            IUserNewsReactionRepository userNewsReactionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _logger = logger;
            _newsRepository = newsRepository;
            _userNewsReactionRepository = userNewsReactionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // 
        public CustomResponse<NewsDetailResponseDto> GetNewsDetail(NewsRequestDto newsDto)
        {
            NewsDetailResponseDto newsResponse = new();
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            try
            {
                var result = Validation(newsDto, _logger);

                if (result != null)
                {
                    if (result.Status == false)
                    {
                        return CustomResponse<NewsDetailResponseDto>.Fail(result.ErrorEnums, false);
                    }
                }

                // Mapleme işlemi yapıldı
                News news = _mapper.Map<News>(newsDto);
                // Gelen id ye karşılık bir id değeri veritabanında getiriliyor.
                var resultNews = _newsRepository.GetById(news.Id);
                Debug.WriteLine("News  " + resultNews);

                // If data from database is null
                if (resultNews == null)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIsNotRegister);
                    return CustomResponse<NewsDetailResponseDto>.Fail(_enumListErrorResponse, false);
                }


                // News id sini kullanarak action type ı alıcam

                var userNewsReaciton = _userNewsReactionRepository.Get(resultNews.Id);

                // Response dönmek için Mapleme işlemi yaptım
                newsResponse = _mapper.Map<NewsDetailResponseDto>(resultNews);

                if (userNewsReaciton != null)
                {
                    newsResponse.ActionType = userNewsReaciton.ActionType;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(V2 + e.Message, e);
            }
            return CustomResponse<NewsDetailResponseDto>.Success(newsResponse, true);
        }
        public CustomResponse<NewsReactionResponseDto> GetNews(NewsRequestDto newsDto)
        {
            NewsReactionResponseDto newsResponse = new();
            try
            {

                var result = Validation(newsDto, _logger);  // Sonradan yazılan kod

                if (result != null)
                {
                    if (result.Status == false)
                    {
                        return CustomResponse<NewsReactionResponseDto>.Fail(result.ErrorEnums, false);
                    }
                }


                var resultNews = GetNewsDb(newsDto);  // Sonradan yazılan kod

                if (resultNews.Status == false)
                {
                    return CustomResponse<NewsReactionResponseDto>.Fail(resultNews.ErrorEnums, false);
                }
                // News id sini kullanarak action type ı alıcam

                var userNewsReaciton = _userNewsReactionRepository.Get(resultNews.Data.Id);

                // Response dönmek için Mapleme işlemi yaptım
                newsResponse = _mapper.Map<NewsReactionResponseDto>(resultNews);

                if (userNewsReaciton != null)
                {
                    newsResponse.ActionType = userNewsReaciton.ActionType;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(V2 + e.Message, e);
            }
            return CustomResponse<NewsReactionResponseDto>.Success(newsResponse, true);
        }

        // Kullanıcının habere verdiği reaksiyonu kaydetme
        public CustomResponse<CreateActionTypeNewsDto> PostReaction(CreateActionTypeNewsDto createActionTypeNewsDto, int UserId)
        {
            // UserNewsReaction userNewsReaction = new();
            List<ErrorEnumResponse> _enumErrorResponse = new();

            try
            {
                // Validasyon İşlemleri tamamlandı
                var createReactionNewsValidator = new CreateReactionTypeNewsValidation();
                var isValidNews = createReactionNewsValidator.Validate(createActionTypeNewsDto).ThrowIfException();
                if (createActionTypeNewsDto.ActionType != 1 && createActionTypeNewsDto.ActionType != 2 && createActionTypeNewsDto.ActionType != 3)
                {
                    isValidNews.ErrorEnums.Add(ErrorEnumResponse.NewsActionTypeIsFail);
                    isValidNews.Status = false;
                }

                // If there is a validation error
                if (isValidNews.Status == false)
                {
                    if (isValidNews.ErrorEnums.Count >= 1 || !isValidNews.ErrorEnums.IsNullOrEmpty())
                    {
                        return CustomResponse<CreateActionTypeNewsDto>.Fail(isValidNews.ErrorEnums, false, 400);
                    }

                }
                // Check if this id exists in the News table.

                var isExistNews = _newsRepository.GetById(createActionTypeNewsDto.NewsId);

                if (isExistNews == null)
                {
                    _enumErrorResponse.Add(ErrorEnumResponse.NewsIsNotRegister);
                    return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);
                }

                // Böyle bir haber vt de varsa bu haberId ve UserId ye ait bir kayıt var mı User News Reaction tablosunda var mı kontrol edicez 

                var didUserReact = _userNewsReactionRepository.GetUserIdWithNewsId(UserId, createActionTypeNewsDto.NewsId);


                //var isExistNewsReaction = _userNewsReactionRepository.GetById(createActionTypeNewsDto.NewsId);

                //var isExistNewsReaction = _userNewsReactionRepository.GetUserIdWithNewsId(UserId, isExistNews.Id);

                // Eğer User News Reaction tablosundan bir Id değeri gelmiyorsa 
                // Veritabanına yeni kayıt ekliycek ve news de güncekkene yapacak
                if (didUserReact.Id <= 0)
                {
                    try
                    {
                        UserNewsReaction userNewsReaction = _mapper.Map<UserNewsReaction>(createActionTypeNewsDto);
                        if (userNewsReaction == null)
                        {
                            _enumErrorResponse.Add(ErrorEnumResponse.MappingFailed);
                            return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);
                        }
                        userNewsReaction.UserId = UserId;
                        var registerUserNewsReact = _userNewsReactionRepository.Add(userNewsReaction);
                        _userNewsReactionRepository.SaveChanges();

                        var result = UpdateNewsTable(isExistNews, createActionTypeNewsDto.ActionType);
                        if (result.Status == false)
                        {
                            _enumErrorResponse.Add(ErrorEnumResponse.CouldntUpdateActionType);
                            return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);

                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(V1 + e.Message, e);
                        throw new Exception(V1, e);
                    }

                }
                // User News Reaction Tablosunda zaten bir kayıt varsa güncelleme işlemi yapılacak
                else
                {
                    if (createActionTypeNewsDto.ActionType == 1)
                    {
                        UserNewsReaction userNewsReactionUpdate = _userNewsReactionRepository.Get(isExistNews.Id);
                        userNewsReactionUpdate.ActionType = createActionTypeNewsDto.ActionType;
                        _userNewsReactionRepository.SaveChanges();

                        var result = UpdateNewsTable(isExistNews, createActionTypeNewsDto.ActionType);
                        if (result.Status == false)
                        {
                            _enumErrorResponse.Add(ErrorEnumResponse.CouldntUpdateActionType);
                            return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);

                        }
                    }
                    else if (createActionTypeNewsDto.ActionType == 2)
                    {

                        UserNewsReaction userNewsReactionUpdate = _userNewsReactionRepository.Get(isExistNews.Id);
                        userNewsReactionUpdate.ActionType = createActionTypeNewsDto.ActionType;
                        _userNewsReactionRepository.SaveChanges();

                        var result = UpdateNewsTable(isExistNews, createActionTypeNewsDto.ActionType);
                        if (result.Status == false)
                        {
                            _enumErrorResponse.Add(ErrorEnumResponse.CouldntUpdateActionType);
                            return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);

                        }
                    }
                    else
                    {
                        UserNewsReaction userNewsReactionUpdate = _userNewsReactionRepository.Get(isExistNews.Id);
                        userNewsReactionUpdate.ActionType = createActionTypeNewsDto.ActionType;
                        _userNewsReactionRepository.SaveChanges();

                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(V1 + e.Message, e);
            }

            return CustomResponse<CreateActionTypeNewsDto>.Success(true);
        }

        public static CustomResponse<NoContentDto> Validation(NewsRequestDto newsDto, ILogger _logger)
        {
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            try
            {
                var newsValidator = new NewsValidation();
                var isValidNews = newsValidator.Validate(newsDto).ThrowIfException();
                // Validasyonlarda hata varsa 
                if (isValidNews.Status == false)
                {
                    return CustomResponse<NoContentDto>.Fail(isValidNews.ErrorEnums, false);
                }
                // Gelen Id değerinin string olmaması gerekiyor
                bool isIdInteger = newsDto.Id.GetType() == typeof(int);
                if (!isIdInteger)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIdNotString);
                    return CustomResponse<NoContentDto>.Fail(_enumListErrorResponse, false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(" NEWS VALIDATION GET DB ERROR: " + e.Message);
            }

            return CustomResponse<NoContentDto>.Success(true);
        }
        public CustomResponse<News> GetNewsDb(NewsRequestDto newsDto)
        {
            List<ErrorEnumResponse> _enumListErrorResponse = new();
            News resultNews = new();
            try
            {
                // Mapleme işlemi yapıldı
                News news = _mapper.Map<News>(newsDto);
                // Gelen id ye karşılık bir id değeri veritabanında getiriliyor.
                resultNews = _newsRepository.GetById(news.Id);
                Debug.WriteLine("News  " + resultNews);

                // If data from database is null
                if (resultNews == null)
                {
                    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIsNotRegister);
                    return CustomResponse<News>.Fail(_enumListErrorResponse, false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(" NEWS ID GET DB ERROR: " + e.Message);
            }
            return CustomResponse<News>.Success(resultNews, true);
        }

        public CustomResponse<bool> UpdateNewsTable(News news, int actionType)
        {
            try
            {
                if (actionType == 1)
                {
                    news.LikeCount++;
                    _newsRepository.SaveChanges();
                    return CustomResponse<bool>.Success(true);
                }
                if (actionType == 2)
                {
                    news.DislikeCount++;
                    _newsRepository.SaveChanges();
                    return CustomResponse<bool>.Success(true);
                }
            }
            catch (Exception e)
            {

            }

            return CustomResponse<bool>.Success(true);
        }

        public CustomResponse<NewsAllResponseDto> GetAllNews()
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                var getAllNews = _newsRepository.GetAll();
                var totalCount = _newsRepository.NewsTotalCount();
                if (getAllNews == null || totalCount<=0)
                {
                    errors.Add(ErrorEnumResponse.RegisteredNoNews);
                }
                List<GetAllNewsResponseDto> getAllNewsResponses = new();
                foreach(var item in getAllNews)
                {
                    var getAllNewsResponse = _mapper.Map<GetAllNewsResponseDto>(getAllNews);
                    if(getAllNewsResponse == null)
                    {
                        break;
                    }
                    getAllNewsResponses.Add(getAllNewsResponse);
                }
                if(getAllNewsResponses.Count<=0)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<NewsAllResponseDto>.Fail(errors, false);
                }

                NewsAllResponseDto responseNewsList = new()
                {
                    _news = getAllNewsResponses,
                    TotalCount = totalCount,
                };

                if( responseNewsList == null)
                {

                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<NewsAllResponseDto>.Fail(errors, false);
                }

                return CustomResponse<NewsAllResponseDto>.Success(responseNewsList,true);
            }
            catch(Exception e)
            {

                _logger.LogError(" GetAllNews METHOD  Log Error: " + e.Message, e);
                return CustomResponse<NewsAllResponseDto>.Fail(errors, false);
            }
        }

        public CustomResponse<List<GetLastNewsResponseDto>> GetLast2News()
        {
            List<ErrorEnumResponse> errors = new();
            try
            {
                var getLast2News = _newsRepository.GetLast2News();

                if(getLast2News == null)
                {
                    errors.Add(ErrorEnumResponse.RegisteredNoNews);
                    return CustomResponse<List<GetLastNewsResponseDto>>.Fail(errors, false);
                }

                List<GetLastNewsResponseDto> getLastNewsResponses = new();
                foreach (var item in getLast2News)
                {
                    var mapp = _mapper.Map<GetLastNewsResponseDto>(item);
                    if (mapp == null)
                        break;
                    getLastNewsResponses.Add(mapp);
                }

                if(getLastNewsResponses.Count<=0)
                {
                    errors.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<List<GetLastNewsResponseDto>>.Fail(errors, false);
                }

                return CustomResponse<List<GetLastNewsResponseDto>>.Success(getLastNewsResponses,true);
            }
            catch(Exception ex)
            {
                _logger.LogError(" GetLast2News METHOD  Log Error: " + ex.Message, ex);
                return CustomResponse<List<GetLastNewsResponseDto>>.Fail(errors, false);
            }
        }
    }
}


//var newsValidator = new NewsValidation();
//var isValidNews = newsValidator.Validate(newsDto).ThrowIfException();
//// Validasyonlarda hata varsa 
//if (isValidNews.Status == false)
//{
//   return CustomResponse<NewsDetailResponseDto>.Fail(isValidNews.ErrorEnums, false);
//}    
//// Gelen Id değerinin string olmaması gerekiyor
//bool isIdInteger = newsDto.Id.GetType() == typeof(int);
//if (!isIdInteger)
//{
//    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIdNotString);
//    return CustomResponse<NewsDetailResponseDto>.Fail(_enumListErrorResponse, false);
//}



// Mapleme işlemi yapıldı
//News news = _mapper.Map<News>(newsDto);
//// Gelen id ye karşılık bir id değeri veritabanında getiriliyor.
//var resultNews = _newsRepository.GetById(news.Id);
//Debug.WriteLine("News  " + resultNews);