namespace BackendBarbaEmDia.Domain.Models.Responses
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool ExceptionGenerated { get; set; }

        public ServiceResult(string message, bool success = true, bool exptionGenerated = false)
        {
            Success = success;
            Message = message;
            ExceptionGenerated = exptionGenerated;
        }

        public ServiceResult(bool success, string message, bool exptionGenerated = false)
        {
            Success = success;
            Message = message;
            ExceptionGenerated = exptionGenerated;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public ServiceResult(T data, string message = "", bool success = true, bool exptionGenerated = false)
            : base(message, success, exptionGenerated)
        {
            Data = data;
        }
        public ServiceResult(bool success, string message = "", bool exptionGenerated = false)
            : base(message, success, exptionGenerated)
        {
        }
    }
}
