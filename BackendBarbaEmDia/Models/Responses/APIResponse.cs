using BackendBarbaEmDia.Domain.Models.Responses;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Models.Responses
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public APIResponse(string message, bool success = true)
        {
            Success = success;
            Message = message;
        }

        public APIResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }


        public static implicit operator APIResponse(ServiceResult serviceResult)
        {
            return new APIResponse(serviceResult.Message, serviceResult.Success);
        }
    }

    public class APIResponse<T> : APIResponse
    {        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public APIResponse(T? data, string message = "", bool success = true) : base(message, success)
        {
            Data = data;
        }        

        public static implicit operator APIResponse<T>(ServiceResult<T> serviceResult)
        {
            return new APIResponse<T>(serviceResult.Data, serviceResult.Message, serviceResult.Success);
        }
    }
}
