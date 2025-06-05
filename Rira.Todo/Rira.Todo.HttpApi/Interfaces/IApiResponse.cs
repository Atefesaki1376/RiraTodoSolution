namespace Rira.Todo.HttpApi.Interfaces
{
    public interface IApiResponse
    {
        Task<T> ResponseHandlerAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default);

        Task ResponseHandlerAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default);

        Task<T> ResponseHandlerGenericAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default);
    }
}