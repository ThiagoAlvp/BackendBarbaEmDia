using System.Text.Json.Serialization;

namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class APIResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public APIResponse(T data, string message = "", bool success = true)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public APIResponse(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = null;
        }
    }
}
