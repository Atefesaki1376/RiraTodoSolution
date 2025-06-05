namespace Rira.Todo.EFCore.Configurations
{
    internal class AppSettingsConfiguration : ConfigurationBase<AppSettings, Guid>
    {
        public override void Configure(EntityTypeBuilder<AppSettings> builder)
        {
            builder.ToTable(nameof(AppSettings), "dbo");

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(e => e.Theme)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.Language)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.Version)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.Username)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.StorageLocation)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.CurrentPlan)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.HelpCenterURL)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(e => e.FeedbackEmail)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasData(DataSeed());

            base.Configure(builder);
        }

        private AppSettings DataSeed()
            => new AppSettings
            {
                Id = Guid.Parse("3f9a8dbf-78c9-4ef6-bdbc-98f7802db983"),
                Name = "DefaultSettings",
                Theme = "Light",
                Language = "English",
                Version = "1.0.0",
                NotificationsEnabled = true,
                Username = "admin",
                Email = "admin@app.com",
                TwoFactorAuthenticationEnabled = false,
                BatterySaverMode = false,
                StorageLocation = "Local",
                CurrentPlan = "Free",
                PlanRenewalDate = new DateTime(2025, 04, 01, 10, 00, 00),
                HelpCenterURL = "https://help.app.com",
                FeedbackEmail = "feedback@app.com"
            };
    }
}