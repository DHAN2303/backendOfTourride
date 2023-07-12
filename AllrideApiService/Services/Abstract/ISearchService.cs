using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiService.Response;

namespace AllrideApiService.Services.Abstract
{
    public interface ISearchService
    {
        public CustomResponse<List<ClubResponseDto>> GetClub(string input);
    }
}
