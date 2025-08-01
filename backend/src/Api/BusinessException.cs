using Api.Models;

namespace Api
{
    public class BusinessException : ApplicationException
    {
        public ErrorResponse ErrorResponse { get; private set; }

        public BusinessException() { }

        public BusinessException(string message)
            : base(message) { }

        public BusinessException(string message, Exception inner)
            : base(message, inner) { }

        public BusinessException(ErrorResponse errorResponse)
           : this(errorResponse.Message)
        {
            ErrorResponse = errorResponse;
        }
    }
}
