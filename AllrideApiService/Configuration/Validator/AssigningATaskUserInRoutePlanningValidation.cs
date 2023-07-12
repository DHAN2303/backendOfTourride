using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class AssigningATaskUserInRoutePlanningValidation : AbstractValidator<AddFriendsTasksRoutePlannerDto>
    {
        public AssigningATaskUserInRoutePlanningValidation()
        {
            RuleFor(x => x.RoutePlannerId)
            .NotNull()
            .WithErrorCode("768")
            .NotEmpty()
            .WithErrorCode("769");

            RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithErrorCode("770")
            .MinimumLength(3)
            .WithErrorCode("771");

            RuleFor(x => x.FriendsAndTasksId)
            .NotNull()
            .WithErrorCode("772")
            .NotEmpty()
            .WithErrorCode("773");
        }
    }
}
