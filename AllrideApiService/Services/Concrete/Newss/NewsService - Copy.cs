//using AllrideApiCore.Dtos;
//using AllrideApiCore.Dtos.Insert;
//using AllrideApiCore.Dtos.Select;
//using AllrideApiCore.Entities;
//using AllrideApiRepository.Repositories.Abstract;
//using AllrideApiRepository.Repositories.Concrete;
//using AllrideApiService.Abstract;
//using AllrideApiService.Configuration.Extensions;
//using AllrideApiService.Configuration.Validator;
//using AllrideApiService.Response;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using Nest;
//using System.Diagnostics;
//using System.Security.Claims;

//namespace AllrideApiService.Concrete
//{
//    public class NewsService: INewsService
//    {
//        private const string V = "News Service Katmanı PostNews Metodu Error Log Kaydı: ";
//        private const string V1 = " USER NEWS REACTION DID'NT REGISTER:  ";
//        private const string V2 = " News Service Log Error: ";
//        private readonly INewsRepository _newsRepository;
//        private readonly IUserNewsReactionRepository _userNewsReactionRepository;
//        private readonly IMapper _mapper;
//        private readonly ILogger<NewsService> _logger;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        public NewsService(INewsRepository newsRepository,
//            IMapper mapper, ILogger<NewsService> logger, 
//            IUserNewsReactionRepository userNewsReactionRepository, IHttpContextAccessor httpContextAccessor)
//        {
//            _mapper = mapper;
//            _logger = logger;
//            _newsRepository = newsRepository;
//            _userNewsReactionRepository= userNewsReactionRepository;
//            _httpContextAccessor= httpContextAccessor;
//        }

//        // 
//        public CustomResponse<NewsResponseDto> GetNews(NewsDto newsDto)
//        {
//            NewsResponseDto newsResponse = new();
//            List<ErrorEnumResponse> _enumListErrorResponse = new();
//            try
//            {
//                var newsValidator = new NewsValidation();
//                var isValidNews = newsValidator.Validate(newsDto).ThrowIfException();
//                // Validasyonlarda hata varsa 
//                if (isValidNews.Status == false)
//                {
//                   return CustomResponse<NewsResponseDto>.Fail(isValidNews.ErrorEnums, false);
//                }    
//                // Gelen Id değerinin string olmaması gerekiyor
//                bool isIdInteger = newsDto.Id.GetType() == typeof(int);
//                if (!isIdInteger)
//                {
//                    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIdNotString);
//                    return CustomResponse<NewsResponseDto>.Fail(_enumListErrorResponse, false);
//                }
//                // Mapleme işlemi yapıldı
//                News news = _mapper.Map<News>(newsDto);
//                // Gelen id ye karşılık bir id değeri veritabanında getiriliyor.
//                var resultNews = _newsRepository.GetById(news.Id);
//                Debug.WriteLine("News  " + resultNews);

//                // If data from database is null
//                if(resultNews == null)
//                {
//                    _enumListErrorResponse.Add(ErrorEnumResponse.NewsIsNotRegister);
//                    return CustomResponse<NewsResponseDto>.Fail(_enumListErrorResponse, false);
//                }


//                // News id sini kullanarak action type ı alıcam

//                var userNewsReaciton = _userNewsReactionRepository.Get(resultNews.Id);

//                // Response dönmek için Mapleme işlemi yaptım
//                newsResponse = _mapper.Map<NewsResponseDto>(resultNews);

//                if (userNewsReaciton != null)
//                {
//                    newsResponse.ActionType = userNewsReaciton.ActionType;
//                }

//            }
//            catch (Exception e)
//            {
//                _logger.LogError(V2 + e.Message, e);
//            }
//            return CustomResponse<NewsResponseDto>.Success(newsResponse, true);
//        }

//        public CustomResponse<CreateActionTypeNewsDto> PostReaction(CreateActionTypeNewsDto createActionTypeNewsDto)
//        {
//            UserNewsReaction userNewsReaction = new();
//            List<ErrorEnumResponse> _enumErrorResponse = new();
//            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            bool isUserIdTypeInt = int.TryParse(userId, out int UserId);
//            try
//            {
//                if(isUserIdTypeInt == false)
//                {
//                    _enumErrorResponse.Add(ErrorEnumResponse.UserIdNotFound);
//                    return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse,false);
//                }
//                // Validasyon İşlemleri tamamlandı
//                var createReactionNewsValidator = new CreateReactionTypeNewsValidation();
//                var isValidNews = createReactionNewsValidator.Validate(createActionTypeNewsDto).ThrowIfException();
//                if (createActionTypeNewsDto.ActionType != 1 && createActionTypeNewsDto.ActionType != 2)
//                {
//                    isValidNews.ErrorEnums.Add(ErrorEnumResponse.NewsActionTypeIsFail);
//                    isValidNews.Status = false;
//                }

//                // If there is a validation error
//                if (isValidNews.Status == false)
//                {
//                    if (isValidNews.ErrorEnums.Count >= 1 || !isValidNews.ErrorEnums.IsNullOrEmpty())
//                    {
//                        return CustomResponse<CreateActionTypeNewsDto>.Fail(isValidNews.ErrorEnums, false);
//                    }

//                }
//                // Check if this id exists in the News table.
//                var isExistNews = _newsRepository.GetById(createActionTypeNewsDto.NewsId);

//                if (isExistNews == null)
//                {
//                    _enumErrorResponse.Add(ErrorEnumResponse.NewsIsNotRegister);
//                    return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);
//                }

//                //var isExistNewsReaction = _userNewsReactionRepository.GetById(createActionTypeNewsDto.NewsId);

//                var isExistNewsReaction =  _userNewsReactionRepository.GetUserIdWithNewsId(UserId, isExistNews.Id);

//                // Eğer User News Reaction tablosundan bir Id değeri gelmiyorsa 

//                if (isExistNewsReaction.Id<=0)
//                {
//                    try
//                    {
//                        UserNewsReaction userNewsReaction1 = _mapper.Map<UserNewsReaction>(createActionTypeNewsDto); 
//                        if (userNewsReaction1 != null)
//                        {
//                            _enumErrorResponse.Add(ErrorEnumResponse.MappingFailed);
//                            return CustomResponse<CreateActionTypeNewsDto>.Fail(_enumErrorResponse, false);
//                        }
//                        userNewsReaction1.UserId = UserId;
//                        var registerUserNewsReact = _userNewsReactionRepository.Add(userNewsReaction1);
//                    }
//                    catch (Exception e)
//                    {
//                        _logger.LogError(V1 + e.Message, e);
//                        throw new Exception(V1, e); 
//                    }

//                }
//                else
//                {
//                    if (createActionTypeNewsDto.ActionType == 1)
//                    {
//                        UserNewsReaction userNewsReactionUpdate = _userNewsReactionRepository.Get(isExistNews.Id);
//                        userNewsReactionUpdate.ActionType = createActionTypeNewsDto.ActionType;
//                        _userNewsReactionRepository.SaveChanges();

//                        isExistNews.LikeCount++;
//                        _newsRepository.SaveChanges();
//                    }
//                    else if (createActionTypeNewsDto.ActionType == 2)
//                    {

//                        UserNewsReaction userNewsReactionUpdate = _userNewsReactionRepository.Get(isExistNews.Id);
//                        userNewsReactionUpdate.ActionType = createActionTypeNewsDto.ActionType;
//                        _userNewsReactionRepository.SaveChanges();

//                        isExistNews.DislikeCount++;
//                        _newsRepository.SaveChanges();
//                    }
//                }

//            }
//            catch (Exception e)
//            {
//                _logger.LogError(V1+e.Message, e);
//            }

//            return CustomResponse<CreateActionTypeNewsDto>.Success(true);
//        }

//        public News PostNews(News news) 
//        {
//            News newsNew = new();
//            try
//            {
//                newsNew = _newsRepository.Post(news);
//                _newsRepository.SaveChanges();
//            }
//            catch(Exception ex)
//            {
//                // Loglar buraya yazılacak
//                _logger.LogError(V + ex.Message);
//            }
//            return newsNew;                        
//        }

//    }
//}





////// Böyle bir kayıt yoksa
////if (isRegisterUserNewsReaction == null)
////{
////    // Action Type 0 ise böyle bir kayıt eklemiycek
////    if (createActionTypeNewsDto.ActionType == 0)
////    {
////        // Yeni bir kayıt oluşturmaya gerek yok
////    }
////    else if (createActionTypeNewsDto.ActionType == 1)
////    {
////        userNewsReaction.UserId = 48; // Normalde burası token kullanıcısından gelen 
////        userNewsReaction.NewsId = isExistNews.Id;
////        userNewsReaction.ActionType = 1;  //
////        userNewsReaction.CreatedDate = DateTime.Now;
////        _userNewsReactionRepository.SaveChanges();
////    }
////    else
////    {
////        userNewsReaction.UserId = 48; // Normalde burası token kullanıcısından gelen 
////        userNewsReaction.NewsId = isExistNews.Id;
////        userNewsReaction.ActionType = 2;
////        userNewsReaction.CreatedDate = DateTime.Now;
////        _userNewsReactionRepository.SaveChanges();
////    }
////    // Action type ne ise o kaydı ekleyecek
////}

////// Böyle bir kayıt varsa action type ' ı gelen action type ile değiştiricem
////if (isRegisterUserNewsReaction.ActionType == createActionTypeNewsDto.ActionType)
////{
////    // Action typelar aynı ise veritabanıdan bir değişiklik olmayacak
////}

////isRegisterUserNewsReaction.ActionType = createActionTypeNewsDto.ActionType;
////_userNewsReactionRepository.SaveChanges();

////return CustomResponse<CreateActionTypeNewsDto>.Success(true);



////if(NewsId == null || ActionType != 0 || ActionType != 1 || ActionType != 2)
////{
////     return CustomResponse<CreateReactionTypeNewsDto>.Fail(false, EnumResponse);
////}
////else


////public CustomResponse<NewsDto> GetNews(NewsDto newsDto)
////{
////    try
////    {
////        var newsValidator = new NewsValidation();
////        var isValidNews = newsValidator.Validate(newsDto).ThrowIfException();

////        if (isValidNews.Status == false)
////        {
////            return CustomResponse<NewsDto>.Fail(false, EnumResponse.NewsIdNullOrEmpty);
////        }

////        News news = _mapper.Map<News>(newsDto);
////        var resultNews = _newsRepository.Get(news);  // Veritabanında olan newsi döndü

////        // Veritabanından  gelen news i Response News e Map le
////    }
////    catch (Exception ex)
////    {

////        Console.WriteLine("News Hata Mesajı: " + ex);
////    }
////    return CustomResponse<NewsDto>.Success(true);
////}

////public CustomResponse<NewsDto> GetLast2News()
////{
////    IEnumerable<News> newsList;
////    NewsDto news = new();
////    try
////    {
////        newsList =  _newsRepository.GetLast2News();
////        news = _mapper.Map<NewsDto>(newsList);
////    }
////    catch (Exception ex)
////    {
////        _logger.LogError("News Service Katmanı GetLast2News Metodu Error Log Kaydı: " + ex.Message);
////    }

////    return CustomResponse<NewsDto>.Success(news,true);
////}


////public CustomResponse<NewsDto> GetNews(NewsDto newsDto)
////{
////    News news = _mapper.Map<News>(newsDto);
////    NewsDto getNewsDto = new();
////    var validator = new NewsValidation();
////    var isValid = validator.Validate(newsDto).ThrowIfException();

////    if (isValid.Errors.Count > 0)
////    {
////        return CustomResponse<NewsDto>.Fail(isValid.Errors);
////    }

////    try
////    {
////        news = _newsRepository.Get(news);
////        if (news == null)
////        {
////            var addFirstNews = PostNews(news);
////            if (addFirstNews != null)
////            {
////                news.Id = addFirstNews.Id;
////                news.Title = addFirstNews.Title;
////                news.LikeCount = addFirstNews.LikeCount;
////                news.UnlikeCount = addFirstNews.UnlikeCount;
////                news.CreatedDate = addFirstNews.CreatedDate;
////                news.UpdatedDate = addFirstNews.UpdatedDate;
////            }
////        }

////        getNewsDto = _mapper.Map<NewsDto>(news);
////    }
////    catch (Exception ex)
////    {
////        // Burada Serilog ile Loglama yapılacak
////        _logger.LogError("News Service Katmanı GetNews Metodu Error Log Kaydı: " + ex.Message);
////    }

////    return CustomResponse<NewsDto>.Success(getNewsDto, true);
////}
