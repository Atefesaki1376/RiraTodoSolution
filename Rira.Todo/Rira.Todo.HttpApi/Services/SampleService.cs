namespace Rira.Todo.HttpApi.Services
{
    public class SampleService(
        ILogger<SampleService> logger,
        HttpClient httpClient,
        IApiResponse apiResponse)
    {
        const string url = "https://jsonplaceholder.typicode.com/todos/1";

        public async Task<SampleDto> GetAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await httpClient.GetAsync(url, cancellationToken);
                var result = await apiResponse.ResponseHandlerAsync<SampleDto>(response, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogException(ex, ex.Message);
                throw;
            }
        }
    }
}