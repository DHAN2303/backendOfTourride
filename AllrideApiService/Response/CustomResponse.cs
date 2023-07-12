using AllrideApiService.Enums;
using System.Text.Json.Serialization;

namespace AllrideApiService.Response
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public string Value { get; set; }

        public List<ErrorEnumResponse> ErrorEnums { get; set; }
        public ErrorEnumResponse ErrorEnum { get; set; }
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
        public static CustomResponse<T> Fail(ErrorEnumResponse errorEnum, bool status)
        {
            return new CustomResponse<T> { ErrorEnum = errorEnum, Status = status };
        }

        public static CustomResponse<T> Fail(List<string> errors)
        {
            return new CustomResponse<T> { Errors = errors };
        }

       
        public static CustomResponse<T> Fail(string error)
        {
            return new CustomResponse<T> {Errors = new List<string> { error } }; 
        }
        public static CustomResponse<T> Fail(int status, string error)
        {
            return new CustomResponse<T> { StatusCode = status, Errors = new List<string> { error }};
        }
        public static CustomResponse<T> Fail(T Data, List<ErrorEnumResponse> listErrorEnums, bool status)
        {
            return new CustomResponse<T> { Data = Data, ErrorEnums = listErrorEnums, Status = status };
        }

        public static CustomResponse<T> Warning(T Data, List<ErrorEnumResponse> listErrorEnums, List<SuccessEnumResponse> successEnumResponses, bool status)
        {
            return new CustomResponse<T> { Data = Data, ErrorEnums = listErrorEnums, SuccessEnums = successEnumResponses, Status = status };
        }

        public static CustomResponse<T> Fail(List<ErrorEnumResponse> erors)
        {
            return new CustomResponse<T> { ErrorEnums = erors };
        }

  
    }
}
