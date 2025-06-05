namespace Rira.Todo.HttpApi.Services
{
    public class ApiExceptionHandler : IApiExceptionHandler
    {
        private readonly ILogger<ApiExceptionHandler> _logger;
        private readonly IStringLocalizer<AppResource> _localization;

        public ApiExceptionHandler(
            ILogger<ApiExceptionHandler> logger,
            IStringLocalizer<AppResource> localization)
        {
            _logger = logger;
            _localization = localization;
        }

        public virtual void ResultErrorHandler(
            IResultModel? result,
            HttpStatusCode httpStatusCode)
        {
            List<string> errors = new();

            if (result?.IsSuccess == false)
            {
                errors.AddRange(result.Errors);
                string content = result?.ToString() ?? "";
                throw new ApiException(content, httpStatusCode, errors);
            }

            errors.Add(_localization[MessageResource.ServerNoResponse]);

            throw new ApiException(
                _localization[MessageResource.ServerNoResponse],
                httpStatusCode,
                errors);
        }

        public virtual async Task ErrorHandlerAsync(
            HttpResponseMessage response,
            CancellationToken cancellationToken = default)
        {
            HttpStatusCode httpStatusCode = response.StatusCode;
            string content = string.Empty;
            List<string> errors = new List<string>();

            if (httpStatusCode == HttpStatusCode.UnsupportedMediaType)
            {
                content = await response.Content.ReadAsStringAsync(cancellationToken);

                _logger.LogError("HTTP status code: {HttpStatus} ({HttpStatusCode}), {Content}",
                    httpStatusCode,
                    (int)httpStatusCode,
                    content);

                throw new ApiException(
                    content,
                    HttpStatusCode.UnsupportedMediaType,
                    new List<string>
                    {
                    string.Format(_localization[MessageResource.UnsupportedMediaTypeMessage],
                        response.Content.Headers?.ContentType?.MediaType,
                        response.StatusCode)
                    });
            }
            else if (httpStatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError(
                    "Status code: {HttpStatusCode} ({StatusCodeValue}), errors: Web API address not found. Check URL: '{RequestUri}'",
                    httpStatusCode,
                    (int)httpStatusCode,
                    response.RequestMessage?.RequestUri);

                content = _localization[MessageResource.InvalidRequest];
                errors.Add(content);

                throw new ApiException(content, HttpStatusCode.NotFound, errors);
            }
            else if (httpStatusCode == HttpStatusCode.BadRequest ||
                     httpStatusCode == HttpStatusCode.Unauthorized ||
                     httpStatusCode == HttpStatusCode.Forbidden)
            {
                ResultModel? result = await response.Content.ReadFromJsonAsync<ResultModel>(cancellationToken);

                _logger.LogError(
                    "Status code: {HttpStatusCode} ({StatusCodeValue}), errors: {Result}",
                    httpStatusCode,
                    (int)httpStatusCode,
                    result?.ToString());

                if (result?.IsSuccess == false)
                {
                    errors.AddRange(result.Errors ?? new List<string>());
                }
                content = result?.ToString() ?? string.Empty;
            }
            else
            {
                _logger.LogError(
                    "Status code: {HttpStatusCode} ({StatusCodeValue})",
                    httpStatusCode,
                    (int)httpStatusCode);

                content = await response.Content.ReadAsStringAsync(cancellationToken);
            }

            // Throw exception for unhandled cases
            throw new ApiException(content, response.StatusCode, errors);
        }
    }
}