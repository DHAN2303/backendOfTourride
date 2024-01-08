using AllrideApiService.Enums;
using System.Text.Json.Serialization;

namespace AllrideApiService.Response
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public string Value { get; set; }

        public List<ErrorEnumResponse> ErrorEnums { get; set; }
        public List<SuccessEnumResponse> SuccessEnums { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

       // [JsonIgnore]
        public bool Status { get; set; }

        [JsonIgnore]
        public bool ServisLimit { get; set; }

        [JsonIgnore]
        public List<string> Errors { get; set; }

        [JsonIgnore]
        public List<int> ErrorsCodes { get; set; }

        //[JsonIgnore]
        //public EnumResponse errorEnum { get; set; }

        //public List<EnumResponse> ErrorEnum { get; set; }

        public static CustomResponse<T> Success(T data,bool status)
        {
            return new CustomResponse<T> { Data= data, Status = status };
        }
        public static CustomResponse<T> Success(T data, bool status, bool servisLimit)
        {
            return new CustomResponse<T> { Data = data, Status = status, ServisLimit = servisLimit };
        }

        public static CustomResponse<T> Success(bool status)
        {
            return new CustomResponse<T> { Status = status };
        }
        public static CustomResponse<T> Success(bool status, SuccessEnumResponse successEnum)
        {
            return new CustomResponse<T> { Status = status };
        }
        public static CustomResponse<T> Success(string value, bool status)
        {
            return new CustomResponse<T> { Value = value, Status = status };
        }
        public static CustomResponse<T> Fail(List<ErrorEnumResponse> listErrorEnums, bool status, int statusCode)
        {
            return new CustomResponse<T> { ErrorEnums = listErrorEnums, Status = status, StatusCode = statusCode};
        }
        public static CustomResponse<T> Fail(List<ErrorEnumResponse> listErrorEnums, bool status)
        {
            return new CustomResponse<T> { ErrorEnums = listErrorEnums, Status = status};
        }

        // Bu yapılar Factory Design Pattern'ından Static Factory Metotlardır
        // new anahtar sözcüğü kullanmadan nesne üretme operasyonunu burada  gerçekleştiriyoruz
        public static CustomResponse<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponse<T> { StatusCode = statusCode, Errors = errors};
        }
        public static CustomResponse<T> Fail(List<string> errors)
        {
            return new CustomResponse<T> { Errors = errors };
        }
        public static CustomResponse<T> Fail(List<int> errors)
        {
            return new CustomResponse<T> { ErrorsCodes = errors };
        }
        public static CustomResponse<T> Fail(string error)
        {
            return new CustomResponse<T> {Errors = new List<string> { error } }; 
        }
        public static CustomResponse<T> Fail(int status, string error)
        {
            return new CustomResponse<T> { StatusCode = status, Errors = new List<string> { error }};
        }
        public static CustomResponse<T> Fail(bool status, List<string> erors )
        {
            return new CustomResponse<T> { Status = status, Errors = erors };
        }
        //public static CustomResponse<T> Fail(bool status, EnumResponse errorEnums)
        //{
        //    return new CustomResponse<T> { Status = status, errorEnum = errorEnums };
        //}
        public static CustomResponse<T> Fail(List<ErrorEnumResponse> erors)
        {
            return new CustomResponse<T> { ErrorEnums = erors };
        }
    }
}
