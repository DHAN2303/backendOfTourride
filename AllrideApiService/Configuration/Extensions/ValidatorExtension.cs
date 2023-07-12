using AllrideApiCore.Dtos;
using AllrideApiService.Response;
using FluentValidation.Results;

namespace AllrideApiService.Configuration.Extensions
{
    public static class ValidatorExtension
    {
        public static CustomResponse<NoContentDto> ThrowIfException(this ValidationResult validationResult)
        {
            List<ErrorEnumResponse> enumList = new();
            try
            {
                if (validationResult.IsValid)
                   return CustomResponse<NoContentDto>.Success(true);
                var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorCode));
                // Aşağıdaki yöntem olmazsa mesaj içerisindeki rakamları arayıp errocode a ekliycem
                //if(messag)
                var Error = validationResult.Errors.Select(x => x.ErrorCode).ToList();
                foreach (string str in Error)
                {
                    if (Enum.TryParse(str, out ErrorEnumResponse enumValue))
                    {
                        enumList.Add(enumValue);
                    }
                }
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }

            return CustomResponse<NoContentDto>.Fail(enumList,false);
        }


    }
}

//if (validationResult.IsValid)
//    baseResponse.Status = true;
//var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorCode));
//// Aşağıdaki yöntem olmazsa mesaj içerisindeki rakamları arayıp errocode a ekliycem

//var Error = validationResult.Errors.Select(x => x.ErrorCode).ToList();
//bool Status = Error.Count <= 0;
//List<string> errors = new();
//foreach (var failure in validationResult.Errors)
//{
//    if (Int32.TryParse(failure.ErrorCode, out int Num))
//        errors.Add(failure.ErrorCode);
//}
//baseResponse.Status = Status;
//baseResponse.ErrorCode = Error;

//public static BaseResponse ThrowIfException(this ValidationResult validationResult)
//{
//    BaseResponse baseResponse = new();
//    if (validationResult.IsValid)
//        baseResponse.Status = true;
//    var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorCode));
//    // Aşağıdaki yöntem olmazsa mesaj içerisindeki rakamları arayıp errocode a ekliycem

//    var Error = validationResult.Errors.Select(x => x.ErrorCode).ToList();
//    bool Status = Error.Count <= 0;
//    List<string> errors = new();
//    foreach (var failure in validationResult.Errors)
//    {
//        if (Int32.TryParse(failure.ErrorCode, out int Num))
//            errors.Add(failure.ErrorCode);  
//    }
//    baseResponse.Status = Status;
//    baseResponse.ErrorCode = Error;
//    return baseResponse;
//}
//public static IDictionary<string, string[]> ThrowIfException(this ValidationResult validationResult, ModelStateDictionary modelState)
//{

//    return validationResult.Errors
//   .GroupBy(x => x.PropertyName)
//   .ToDictionary(
//     g => g.Key,
//     g => g.Select(x => x.ErrorMessage).ToArray()
//   );

//    //var err = validationResult.Errors.Select(validationResult => validationResult.ErrorCode);
//    //Debug.WriteLine("Hata Kodlarını döndürme:" + err);
//    //if (validationResult.IsValid)
//    //    return new BaseResponse()
//    //    {
//    //        Status = true,
//    //    };

//    //var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorCode));
//    //var Error = validationResult.Errors.Select(x => x.ErrorCode).ToList();
//    //return new BaseResponse()
//    //{
//    //    ErrorCode = Error,
//    //    Status = false,
//    //   // ErrorCode = message. // Nuraya Validasyonlardan dönen hata kodu gelecek
//    //};


//}


//public static BaseResponse ThrowIfException(this ValidationResult validationResult)
//{
//    BaseResponse baseResponse = new();

//    try
//    {
//        if (validationResult.IsValid)
//            baseResponse.Status = true;
//        var message = string.Join(',', validationResult.Errors.Select(x => x.ErrorCode));
//        // Aşağıdaki yöntem olmazsa mesaj içerisindeki rakamları arayıp errocode a ekliycem

//        var Error = validationResult.Errors.Select(x => x.ErrorCode).ToList();
//        bool Status = Error.Count <= 0;
//        List<int> errors = new();
//        foreach (var failure in validationResult.Errors)
//        {
//            if (Int32.TryParse(failure.ErrorCode, out int Num))
//                errors.Add(Num);
//        }
//        baseResponse.Status = Status;
//        baseResponse.Errors = errors;
//    }

//    catch (Exception ex) {


//    }

//    return baseResponse;
//}