using System.Net;

namespace ContasBancarias.Domain.Entities.Api
{
    public class ApiResponse
    {
        public string Content { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public Exception ErrorException { get; private set; }
        public string ErrorMessage { get; private set; }
        public Uri ResponseUri { get; private set; }

        public ApiResponse(string content, HttpStatusCode statusCode, bool success, Exception errorException, string errorMessage,
            Uri responseUri)
        {
            Content = content;
            StatusCode = statusCode;
            Success = success;
            ErrorException = errorException;
            ErrorMessage = errorMessage;
            ResponseUri = responseUri;
        }
    }
}
