using AllrideApiCore.Dtos.RequestDto.RoutePlanner;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class CreateRoutePlannerForGroupDtoValidation : AbstractValidator<CreateRoutePlannerForGroupDto>
    {
        public CreateRoutePlannerForGroupDtoValidation()
        {
            RuleFor(x => x.GroupId).NotNull().WithErrorCode("919");
            RuleFor(x => x.GroupId).NotNull().WithErrorCode("920");
            RuleFor(x => x.RoutePlannerTitle).NotEmpty().WithErrorCode("751");
            RuleFor(x => x.RoutePlannerTitle).MaximumLength(30).WithErrorCode("752");
            RuleFor(x => x.RoutePlannerTitle).MinimumLength(3).WithErrorCode("753");

            RuleFor(x => x.RouteName).NotNull().WithErrorCode("754");
            RuleFor(x => x.RouteName).NotEmpty().WithErrorCode("755");
            RuleFor(x => x.RouteName).MaximumLength(30).WithErrorCode("756");
            RuleFor(x => x.RouteName).MinimumLength(3).WithErrorCode("757");

            RuleFor(x => x.ColorCodeHex)
                .NotNull().WithErrorCode("762")
                .NotEmpty().WithErrorCode("763")
                .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
                .WithErrorCode("764");

            RuleFor(x => x.StartDate).NotNull().WithErrorCode("765");
            RuleFor(x => x.StartDate).NotEmpty().WithErrorCode("766");
            RuleFor(x => x.EndDate).NotNull().WithErrorCode("767");
            RuleFor(x => x.EndDate).NotEmpty().WithErrorCode("768");

            RuleFor(x => x.RouteId).NotNull().WithErrorCode("777");
            RuleFor(x => x.RouteId).NotEmpty().WithErrorCode("778");
            RuleFor(x => x.RouteId).GreaterThan(0).WithErrorCode("779");
        }
    }
}
