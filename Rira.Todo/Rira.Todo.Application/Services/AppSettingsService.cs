namespace Rira.Todo.Application.Services
{
    public class AppSettingsService :
        AppServiceCrudBase<AppSettings, AppSettingsDto>,
        IAppSettingsService
    {
        private readonly AppSettingsValidator _validator;

        public AppSettingsService(
            ILogger<AppSettingsService> logger,
            IRepository<AppSettings> repository,
            IStringLocalizer<AppResource> localizer,
            IMapper mapper) : base(logger, localizer, repository, mapper)
        {
            _validator = new AppSettingsValidator();
        }

        protected override async Task ValidateAsync(
            AppSettingsDto dto,
            ValidationAppContext context,
            CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(dto, cancellationToken);
        }
    }
}