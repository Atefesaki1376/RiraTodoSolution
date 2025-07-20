using System;

namespace Rira.Todo.EFCore
{
    public class AppDbContext<TUserId> : DbContext
        where TUserId : struct, IEquatable<TUserId>
    {
        private readonly ILogger<AppDbContext<TUserId>> _logger;
        private readonly ICurrentUser<TUserId> _currentUser;

        public AppDbContext(DbContextOptions<GuidAppDbContext> options) : base(options)
        {
            
        }

        public AppDbContext(
            ILogger<AppDbContext<TUserId>> logger,
            DbContextOptions<AppDbContext<TUserId>> options,
            ICurrentUser<TUserId> currentUser) : base(options)
        {
            _logger = logger;
            _currentUser = currentUser;
        }

        public AppDbContext(ILogger<AppDbContext<TUserId>> logger,
            DbContextOptions<AppDbContext<TUserId>> options) : base(options)
        {
            _logger = logger;
        }

        public DbSet<AppSettings> AppSettings { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext<TUserId>).Assembly);

            SoftDeleteFilterQuery(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Auditing();

                int result = await base.SaveChangesAsync(cancellationToken);
                _logger.LogInformation(
                    "Successfully saved changes. Affected entries: {Result}. Initiated by user: {UserId}",
                    result,
                    _currentUser.Id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex, ex.Message);

                throw;
            }

        }

        private void Auditing()
        {
            try
            {
                var entries = ChangeTracker.Entries()
               .Where(e => e.Entity is AuditEntityBase<TUserId> &&
                          (e.State == EntityState.Added ||
                           e.State == EntityState.Modified ||
                           e.State == EntityState.Deleted));

                foreach (var entry in entries)
                {
                    var auditEntity = (AuditEntityBase<TUserId>)entry.Entity;
                    var currentTime = DateTime.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntity.CreatedOn = currentTime;
                            auditEntity.ModifiedOn = currentTime;
                            auditEntity.CreatedBy = _currentUser.Id;
                            auditEntity.ModifiedBy = _currentUser.Id;
                            break;

                        case EntityState.Modified:
                            auditEntity.ModifiedOn = currentTime;
                            auditEntity.ModifiedBy = _currentUser.Id;
                            break;
                    }

                    if (entry.State == EntityState.Deleted &&
                        entry.Entity is ISoftDelete<TUserId> softDeleteEntity)
                    {
                        softDeleteEntity.IsDeleted = true;
                        softDeleteEntity.DeletedAt = currentTime;
                        softDeleteEntity.DeletedBy = _currentUser.Id;

                        entry.State = EntityState.Modified;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Auditing failed. Proceeding with save without auditing.");
            }
        }

        private void SoftDeleteFilterQuery(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete<TUserId>).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var isDeletedProperty = Expression.Property(parameter, nameof(ISoftDelete<TUserId>.IsDeleted));
                    var compareExpression = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                    var lambda = Expression.Lambda(compareExpression, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    }
    // (e)
    // (e.IsSoftDeleted)
    // (e.IsSoftDeleted == false)
    // (e => e.IsSoftDeleted == false)
    public class GuidAppDbContext : AppDbContext<Guid>
    {

        public GuidAppDbContext(DbContextOptions<GuidAppDbContext> options) : base(options)
        {
        }
    }
}