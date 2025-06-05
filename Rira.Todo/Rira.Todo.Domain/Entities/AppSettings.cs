namespace Rira.Todo.Domain.Entities
{
    public class AppSettings : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
        public string Version { get; set; }
        public bool NotificationsEnabled { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public bool TwoFactorAuthenticationEnabled { get; set; }

        public bool BatterySaverMode { get; set; }
        public string StorageLocation { get; set; }

        public string CurrentPlan { get; set; }
        public DateTime PlanRenewalDate { get; set; }

        public string HelpCenterURL { get; set; }
        public string FeedbackEmail { get; set; }

    }
}