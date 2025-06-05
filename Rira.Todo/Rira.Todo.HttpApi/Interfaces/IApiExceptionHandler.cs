namespace Rira.Todo.HttpApi.Interfaces
{
    public interface IApiExceptionHandler
    {
        Task ErrorHandlerAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default);

        void ResultErrorHandler(
            IResultModel? result,
            HttpStatusCode httpStatusCode);
    }
}
