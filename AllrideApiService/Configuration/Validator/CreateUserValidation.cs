using DTO.Insert;
using FluentValidation;

namespace AllrideApiService.Configuration.Validator
{
    public class CreateUserValidation : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.Name).NotNull().WithErrorCode("10");
            RuleFor(x => x.Name).MinimumLength(2).WithErrorCode("11");
            RuleFor(x => x.Name).MaximumLength(30).WithErrorCode("12");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode("13");

            RuleFor(x => x.LastName).NotNull().WithErrorCode("20");
            RuleFor(x => x.LastName).MinimumLength(2).WithErrorCode("21");
            RuleFor(x => x.LastName).MaximumLength(30).WithErrorCode("22");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("23");

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

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .WithErrorCode("40")
                .NotEmpty()
                .WithErrorCode("41");

            // Phone boyutu en az şu kadar olmalı
            RuleFor(x => x.Phone)
                .NotNull()
                .WithErrorCode("50")
                .Matches("^\\+?[1-9][0-9]{7,14}$")
                .WithErrorCode("51")
                .MaximumLength(25)
                .WithErrorCode("53")
                .NotEmpty()
                .WithErrorCode("54");
           // RuleFor(x => x.Phone).NotEmpty().WithMessage("Category name is required.").Must(UniquePhone).WithMessage("This category name already exists.");

            RuleFor(x => x.UserPassword).NotNull().WithErrorCode("60");
            RuleFor(x => x.UserPassword).MinimumLength(8).WithErrorCode("61").
             MaximumLength(16).WithErrorCode("62")
            .Matches("[A-Z]").WithMessage("Bir veya daha fazla büyük harf içermelidir").WithErrorCode("63")
            .Matches("[a-z]").WithMessage("Bir veya daha fazla küçük harf içermelidir").WithErrorCode("64")
            .Matches(@"\d").WithMessage("Bir veya daha fazla sayıda rakam içermelidir").WithErrorCode("65")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Bir veya daha fazla özel karakter içermelidir.")
            .WithErrorCode("66")
            .Matches("^[^£# “”]*$").WithMessage("aşağıdaki karakterleri £ # “” veya boşluk içermemelidir.")
            .WithErrorCode("67"); 
            RuleFor(x => x.UserPassword).NotEmpty().WithErrorCode("68");

            RuleFor(x => x.PasswordConfirm)
                .NotNull()
                .WithErrorCode("70")
                .MinimumLength(8)
                .WithErrorCode("71")
                .MaximumLength(30)
                .WithErrorCode("72")
                .Equal(x => x.PasswordConfirm).WithErrorCode("73")
                .NotEmpty()
                .WithErrorCode("75");

           // RuleFor(x => x.Gender).Must(gender=>gender=="0").Must(g=>g=="1").Must(g=>g=="2").WithErrorCode("80");

            //.Must(pass => !blacklistedWords.Any(word => pass.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
            //    .WithMessage("'{PropertyName}' contains a word that is not allowed.");
            //    }

        }
        //message "NotEmptyValidator,11,NotEmptyValidator,21,31,RegularExpressionValidator,61,MinimumLengthValidator,RegularExpressionValidator,RegularExpressionValidator,RegularExpressionValidator"

        //private bool UniquePhone(CreateUserDto dto, string phone)
        //{
        //    UserDetail userDetail = _mapper.Map<UserDetail>(dto);
        //    var isPhone = _context.user_detail.Where(x => x.Phone == phone).SingleOrDefault();

        //    if (isPhone == null) return true;
        //    return false;
        //}

    }

}



// RuleFor(x => x.PhoneNumber). Userservice de bu telefon nymarası kullanılıyorsa 52 hata kodu dönecek
// Telefon numara formatı Ülke kodu ve o ülkenin telefon basamak koduna göre formatlanıp kayıt edilebilecek burada bir hata varsa 51 hata kodu dönecek
//RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithErrorCode("60");
//RuleFor(x => x.BirthDate).ToString //Burası date formatına uygun mu diye kontrol edicek fluent validationda varsa

// CASE 2:
// LOGIN VALİDASYONU İÇİN BAKMAN GEREKECEK https://docs.fluentvalidation.net/en/latest/start.html#throwing-exceptions
//https://docs.fluentvalidation.net/en/latest/async.html
//public class CustomerValidator : AbstractValidator<Customer>
//{

//UserRepository _userRepository;
//User _user;
//    SomeExternalWebApiClient _client;

//    public CustomerValidator(SomeExternalWebApiClient client)
//    {
//        _client = client;

//        RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
//        {
//            bool exists = await _client.IdExists(id);
//            return !exists;
//        }).WithMessage("ID Must be unique");
//    }
//}


