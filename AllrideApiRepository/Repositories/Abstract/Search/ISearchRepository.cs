using AllrideApiCore.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiRepository.Repositories.Abstract.Search
{
    public interface ISearchRepository
    {
        List<SearchResponseDto> GetAllUserGrup(string input);
    }
}
