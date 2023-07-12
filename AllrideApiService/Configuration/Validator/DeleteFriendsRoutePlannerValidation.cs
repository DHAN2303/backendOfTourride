using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class DeleteFriendsRoutePlannerValidation : AbstractValidator<DeleteFriendsRoutePlanner>
    {
        public DeleteFriendsRoutePlannerValidation()
        {
            RuleFor(x => x.RoutePlannerId)
               .NotNull()
               .WithErrorCode("742")
               .NotEmpty()
               .WithErrorCode("743");
           
            RuleFor(x => x.FriendsId)
             .NotNull()
             .WithErrorCode("744")
             .NotEmpty()
             .WithErrorCode("745");
        }
    }
}
