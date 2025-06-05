namespace Rira.Todo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AppControllerBase : ControllerBase
    {
        protected ILogger<AppControllerBase> Logger { get; init; }
        protected AppControllerBase(ILogger<AppControllerBase> logger)
        {
            Logger = logger;
        }
    }
}