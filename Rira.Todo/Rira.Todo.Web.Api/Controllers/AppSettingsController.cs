namespace Rira.Todo.Web.Api.Controllers
{
    public class AppSettingsController : AppControllerBase
    {
        private readonly IAppSettingsService _appSettingsService;

        public AppSettingsController(
            ILogger<AppSettingsController> logger,
            IAppSettingsService appSettingsService) : base(logger)
        {
            _appSettingsService = appSettingsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            var appSettings = await _appSettingsService.GetAsync(1, cancellationToken);
            var result = new ResultModel<AppSettingsDto>(appSettings);

            return Ok(result);
        }
    }
}