using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract.Search;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class SearchRepository :ISearchRepository
    {
        protected AllrideApiDbContext _context;
        public SearchRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public List<SearchResponseDto> GetAllUserGrup(string input)
        {
            var result = new List<SearchResponseDto>();

            var userResults = _context.user_detail
            .Where(t => t.Name.Contains(input))
            .Where(t => t.LastName.Contains(input))
            .Select(t => new { FullName = t.Name + " " + t.LastName, t.PpPath, t.LastName })
            .ToList();

            var groupResults = _context.groups
            .Where(t => t.name.Contains(input))
            .Select(t => new { t.name, t.image_path})
            .ToList();

            var clubResults = _context.club
                .Where(t => t.name.Contains(input))
                .Select(t => new { t.name, t.profile_path })
                .ToList();

            result.AddRange((IEnumerable<SearchResponseDto>)userResults);
            result.AddRange((IEnumerable<SearchResponseDto>)groupResults);
            result.AddRange((IEnumerable<SearchResponseDto>)clubResults);

            return result;

        }
    }
}
