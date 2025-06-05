namespace Rira.Todo.Application.Services
{
    public abstract class AppServiceBase : IAppServiceBase
    {
        protected ILogger<AppServiceBase> Logger { get; init; }
        protected IStringLocalizer<AppResource> Localizer { get; init; }

        protected AppServiceBase(
            ILogger<AppServiceBase> logger,
            IStringLocalizer<AppResource> localizer)
        {
            Logger = logger;
            Localizer = localizer;
        }
    }
}