namespace Rira.Todo.Application.Contracts.Validators
{
    public class AppSettingsValidator : AbstractValidator<AppSettingsDto>
    {
        public AppSettingsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Theme)
                .NotEmpty();
            RuleFor(x => x.Language)
                .NotEmpty();

            RuleFor(x => x.Version)
                .NotEmpty();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.StorageLocation)
                .NotEmpty();

            RuleFor(x => x.CurrentPlan)
                .NotEmpty();

            RuleFor(x => x.HelpCenterURL)
                .NotEmpty();

            RuleFor(x => x.FeedbackEmail)
                .NotEmpty();

            RuleFor(x => x.NotificationsEnabled)
                .NotNull();

            RuleFor(x => x.TwoFactorAuthenticationEnabled)
                .NotNull();

            RuleFor(x => x.BatterySaverMode)
                .NotNull();

            RuleFor(x => x.PlanRenewalDate)
                .NotNull();
        }
    }
}