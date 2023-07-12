using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class DeleteTaskRoutePlannerValidation : AbstractValidator<DeleteTaskRoutePlannerDto>
    {
        public DeleteTaskRoutePlannerValidation()
        {
            RuleFor(x => x.RoutePlannerId)
            .NotNull()
            .WithErrorCode("743")
            .NotEmpty()
            .WithErrorCode("742")
            .GreaterThanOrEqualTo(0)
            .WithErrorCode("789");

            RuleFor(x => x.TaskId)
            .NotNull()
            .WithErrorCode("737")
            .NotEmpty()
            .WithErrorCode("738")
            .GreaterThanOrEqualTo(0)
            .WithErrorCode("739");
        }
    }
}
