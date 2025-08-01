namespace Api.Models
{
    public class ErrorResponse :IResponse
    {
        public string TimeStamp { get; set; } = string.Empty;

        public string ApplicationName { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
