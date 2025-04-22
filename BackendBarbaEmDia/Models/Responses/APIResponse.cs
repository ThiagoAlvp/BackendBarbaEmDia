using BackendBarbaEmDia.Domain.Models.Responses;
using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Models.Responses
{
    public class APIResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public APIResponse(T? data, string message = "", bool success = true)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public APIResponse(string message, bool success = true)
        {
            Success = success;
            Message = message;
            Data = null;
        }

        public APIResponse(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = null;
        }

        public static implicit operator APIResponse<T>(ServiceResult<T> serviceResult)
        {
            return new APIResponse<T>(serviceResult.Data, serviceResult.Message, serviceResult.Success);
        }

        public static implicit operator APIResponse<T>(ServiceResult serviceResult)
        {
            return new APIResponse<T>(serviceResult.Message, serviceResult.Success);
        }
    }
}
