using System.Net;

namespace infertility_system.Middleware
{
    public class CustomHttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public CustomHttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
