using System.Net;

namespace Rira.Todo.Domain.Shared.Exceptions
{
    public class ApiException : Exception, IApiException
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public List<string> Errors { get; init; }

        public ApiException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            Errors = new();
        }
        public ApiException(string message, HttpStatusCode httpStatusCode, List<string> errors) : this(message, httpStatusCode)
        {
            Errors = errors;
        }
    }
}