using System.Net;

namespace Rira.Todo.Domain.Shared.Interfaces
{
    public interface IApiException
    {
        HttpStatusCode HttpStatusCode { get; init; }
        List<string> Errors { get; init; }
        string Message { get; }
    }
}
