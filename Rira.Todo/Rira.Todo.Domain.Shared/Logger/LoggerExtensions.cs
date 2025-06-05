namespace Microsoft.Extensions.Logging
{
    public static partial class Log
    {
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Error,
            Message = "An error occurred `{Message}`")]
        public static partial void LogException(
            this ILogger logger, Exception exception, string message);
    }
}