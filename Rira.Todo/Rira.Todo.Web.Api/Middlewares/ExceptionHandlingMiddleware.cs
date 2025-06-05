namespace Rira.Todo.Web.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IStringLocalizer<AppResource> _localizer;

        public ExceptionHandlingMiddleware(
            ILogger<ExceptionHandlingMiddleware> logger,
            RequestDelegate next,
            IStringLocalizer<AppResource> localizer)
        {
            _next = next;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = _localizer[MessageResource.InternalServerErrorOccured],
            };

            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    var validationProblemDetails = new ValidationProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = _localizer[MessageResource.ValidationError],
                        Detail = _localizer[MessageResource.InternalServerErrorOccured]
                    };

                    foreach (var error in validationException.Errors)
                    {
                        validationProblemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
                    }

                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(validationProblemDetails));
                    break;

                case Domain.Shared.Exceptions.NotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = notFoundEx.Message;
                    break;

                case Domain.Shared.Exceptions.ConflictException conflictEx:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Detail = conflictEx.Message;
                    break;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    break;
            }

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(problemDetails));

        }
    }
}