using System.Text.Json.Serialization;

namespace AllrideApiService.Response
{
    public class BaseResponse
    {
        [JsonIgnore]
        public bool Status { get; set; } = true;

        [JsonIgnore]
        public string Message { get; set; } 

        public List<int> Errors { get; set; }

        public ErrorEnumResponse Enum { get; set; }

        [JsonIgnore]
        public List<ErrorEnumResponse> ListErrorEnums { get; set; }
    }
}


//public class BaseResponse<T>
//{
//    public BaseResponse(T data)
//    {
//        Data = data;
//    }
//    public BaseResponse(EnumResponse error)
//    {
//        Error = error;
//        Status = false;
//    }

//    public T Data { get; set; }
//    public string Message { get; set; }
//    [JsonIgnore]
//    public bool Status { get; set; } = true;
//    public List<string> ErrorCode { get; set; }
//    public EnumResponse Error { get; set; }
//}