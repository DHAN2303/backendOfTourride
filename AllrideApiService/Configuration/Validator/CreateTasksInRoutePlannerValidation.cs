using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class CreateTasksInRoutePlannerValidation : AbstractValidator<CreateTasksInRoutePlanner>
    {
        public CreateTasksInRoutePlannerValidation()
        {
            RuleFor(x => x.Tasks).NotNull().WithErrorCode("790");
            RuleFor(x => x.Tasks).NotEmpty().WithErrorCode("791");
            RuleFor(x => x.Tasks).MaximumLength(1000).WithErrorCode("792");
            RuleFor(x => x.Tasks).MinimumLength(3).WithErrorCode("793");
        }
    }
}
