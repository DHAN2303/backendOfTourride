using AllrideApiCore.Entities.Users;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class GetUserFriendsConfiguration: AbstractValidator<UserEntity>
        {
            public GetUserFriendsConfiguration()
            {
                RuleFor(x => x.Id).NotEmpty().WithErrorCode("400");
                RuleFor(x => x.Id).NotNull().WithErrorCode("401");
                RuleFor(x => x.Id)
                .Must(age => age is int) // Sadece int tipinde olmalıdır
                .WithErrorCode("602");
            }   
    }
}
