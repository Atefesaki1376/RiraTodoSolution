namespace Rira.Todo.HttpApi.Services
{
    public class ApiResponse : IApiResponse
    {
        protected readonly IApiExceptionHandler _apiExceptionHandler;
        protected readonly ILogger<ApiResponse> _logger;

        public ApiResponse(
            ILogger<ApiResponse> logger,
            IApiExceptionHandler apiExceptionHandler)
        {
            _logger = logger;
            _apiExceptionHandler = apiExceptionHandler;
        }

        public virtual async Task<T> ResponseHandlerAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultModel<T>? result = await response.Content.ReadFromJsonAsync<ResultModel<T>>(cancellationToken);
                    if (result?.IsSuccess == true)
                    {
                        return result.Model;
                    }
                    else
                    {
                        _apiExceptionHandler.ResultErrorHandler(result, response.StatusCode);
                    }
                }
                else
                {
                    await _apiExceptionHandler.ErrorHandlerAsync(response, cancellationToken);
                }

                return default!;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex, ex.Message);
                throw;
            }


        }

        public virtual async Task ResponseHandlerAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultModel? result = await response.Content.ReadFromJsonAsync<ResultModel>(cancellationToken);
                    if (result?.IsSuccess == true)
                    {
                        return;
                    }
                    else
                    {
                        _apiExceptionHandler.ResultErrorHandler(result, response.StatusCode);
                    }
                }
                else
                {
                    await _apiExceptionHandler.ErrorHandlerAsync(response, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<T> ResponseHandlerGenericAsync<T>(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    T? result = await response.Content.ReadFromJsonAsync<T>(cancellationToken);
                    return result!;
                }
                else
                {
                    await _apiExceptionHandler.ErrorHandlerAsync(response, cancellationToken);
                }

                return default!;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex, ex.Message);
                throw;
            }
        }
    }
}
