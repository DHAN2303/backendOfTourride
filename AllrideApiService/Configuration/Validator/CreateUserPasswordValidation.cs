using AllrideApiCore.Dtos.Insert;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class CreateUserPasswordValidation : AbstractValidator<CreateUserResetPasswordDto>       
    {
        public CreateUserPasswordValidation()
        {
            RuleFor(x => x.Email)
              .NotNull()
              .WithErrorCode("30")
              .NotEmpty()
              .WithErrorCode("34");
            RuleFor(x => x.Email).Matches("^[\\w-\\.]{3,}@[\\w-\\.]{3,}\\.[a-zA-Z]{2,}$")
                .WithMessage("Lütfen geçerli bir email adresi girin")
                .WithErrorCode("31")
                .MaximumLength(254)
                .WithErrorCode("33");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithErrorCode("35");

            RuleFor(x => x.Password).NotNull().WithErrorCode("60");
            RuleFor(x => x.Password).MinimumLength(8).WithErrorCode("61").
             MaximumLength(16).WithErrorCode("62")
            .Matches("[A-Z]")
            .WithMessage("Bir veya daha fazla büyük harf içermelidir")
            .WithErrorCode("63")
            .Matches("[a-z]")
            .WithMessage("Bir veya daha fazla küçük harf içermelidir")
            .WithErrorCode("64")
            .Matches(@"\d").WithMessage("Bir veya daha fazla sayıda rakam içermelidir").WithErrorCode("65")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Bir veya daha fazla özel karakter içermelidir.")
            .WithErrorCode("66")
            .Matches("^[^£# “”]*$").WithMessage("aşağıdaki karakterleri £ # “” veya boşluk içermemelidir.")
            .WithErrorCode("67");
            RuleFor(x => x.Password).NotEmpty().WithErrorCode("68");

            RuleFor(x => x.PasswordConfirm)
                .NotNull()
                .WithErrorCode("70")
                .MinimumLength(8)
                .WithErrorCode("71")
                .MaximumLength(30)
                .WithErrorCode("72")
                .Equal(x => x.Password).WithErrorCode("74")
                .NotEmpty()
                .WithErrorCode("75");
        }
    }
}
