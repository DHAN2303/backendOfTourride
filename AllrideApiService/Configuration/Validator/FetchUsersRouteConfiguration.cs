using AllrideApiCore.Dtos.RequestDto;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class FetchUsersRouteConfiguration: AbstractValidator<FetchUsersRouteRequestDto>
    {
        public FetchUsersRouteConfiguration()
        {
            RuleFor(x => x.RouteId).NotEmpty().WithErrorCode("600");
            RuleFor(x => x.RouteId).NotNull().WithErrorCode("601");
            RuleFor(x => x.RouteId)
            .Must(age => age is int) // Sadece int tipinde olmalıdır
            .WithErrorCode("602");
        }
    }
}
