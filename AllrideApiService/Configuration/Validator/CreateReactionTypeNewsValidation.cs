using AllrideApiCore.Dtos.Insert;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class CreateReactionTypeNewsValidation: AbstractValidator<CreateActionTypeNewsDto>
    {
        public CreateReactionTypeNewsValidation()
        {
            RuleFor(x => x.NewsId).NotEmpty().WithErrorCode("400");
            RuleFor(x => x.NewsId).NotNull().WithErrorCode("401");
            RuleFor(x => x.ActionType).NotNull().WithErrorCode("405");
            RuleFor(x => x.ActionType).NotEmpty().WithErrorCode("406");
        }
    }
}
