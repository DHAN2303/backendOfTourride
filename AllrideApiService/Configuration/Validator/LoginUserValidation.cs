using DTO.Select;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class LoginUserValidation : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidation() {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode("30")
                .NotNull()
                .WithErrorCode("34");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithErrorCode("31");
            RuleFor(x => x.Email)
                .Matches("^[\\w-\\.]{3,}@[\\w-\\.]{3,}\\.[a-zA-Z]{2,}$")
                .WithMessage("Lütfen geçerli bir email adresi girin")
                .WithErrorCode("35"); 
            RuleFor(x => x.Password)
                .NotNull().WithErrorCode("60")
                .MinimumLength(8).WithErrorCode("61")
                .MaximumLength(30).WithErrorCode("63")
                .NotEmpty().WithErrorCode("68");

        }
    }
}
