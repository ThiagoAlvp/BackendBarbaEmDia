namespace BackendBarbaEmDia.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception exception)
        {
            var message = exception.Message;
            if (exception.InnerException != null)
            {
                message += " " + exception.InnerException.GetFullMessage();
            }
            return message;
        }
    }
}
