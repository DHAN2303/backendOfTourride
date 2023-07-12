using AllrideApiCore.Dtos.Select;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class NewsValidation : AbstractValidator<NewsRequestDto>
    {
        public NewsValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithErrorCode("400");
            // RuleFor(x => x.Title).MinimumLength(2).WithErrorCode("401");
        }
    }
}
