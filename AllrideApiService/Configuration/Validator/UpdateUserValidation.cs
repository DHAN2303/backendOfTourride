using AllrideApiCore.Dtos.Update;
using DTO.Select;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class UpdateUserValidation: AbstractValidator<ForgotPasswordDto>
    {
        public UpdateUserValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().WithErrorCode("30");
            RuleFor(x => x.Email)
                .Matches("^[\\w-\\.]{3,}@[\\w-\\.]{3,}\\.[a-zA-Z]{2,}$")
                .WithMessage("Lütfen geçerli bir email adresi girin")
                .WithErrorCode("33"); 
        }
    }
}
