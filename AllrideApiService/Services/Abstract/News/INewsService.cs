using AllrideApiCore.Dtos.Insert;
using AllrideApiCore.Dtos.Select;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract.News
{
    public interface INewsService
    {
        public CustomResponse<NewsDetailResponseDto> GetNewsDetail(NewsRequestDto newsDto);
        public CustomResponse<NewsReactionResponseDto> GetNews(NewsRequestDto news);
        public CustomResponse<CreateActionTypeNewsDto> PostReaction(CreateActionTypeNewsDto createActionTypeNewsDto, int UserId);
        public CustomResponse<NewsAllResponseDto> GetAllNews();
        public CustomResponse<List<GetLastNewsResponseDto>> GetLast2News();

    }
}
