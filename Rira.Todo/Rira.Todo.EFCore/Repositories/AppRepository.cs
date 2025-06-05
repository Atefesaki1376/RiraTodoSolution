namespace Rira.Todo.EFCore.Repositories
{
    public class AppRepository<TUserId, TEntity> : IRepository<TEntity>
        where TUserId : struct, IEquatable<TUserId>
        where TEntity : EntityBase, new()
    {
        protected ILogger<AppRepository<TUserId, TEntity>> Logger { get; init; }
        protected AppDbContext<TUserId> AppDbContext { get; init; }

        protected ICurrentUser<TUserId> CurrentUser { get; init; }

        public AppRepository(
            ILogger<AppRepository<TUserId, TEntity>> logger,
            AppDbContext<TUserId> dbContext,
            ICurrentUser<TUserId> currentUser)
        {
            Logger = logger;
            AppDbContext = dbContext;
            CurrentUser = currentUser;
        }

        public TEntity Get<TKey>(TKey id)
        {
            try
            {
                return AppDbContext.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task<TEntity> GetAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await AppDbContext.Set<TEntity>().FindAsync(
                     new object[] { id },
                     cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<TEntity> GetList()
        {
            try
            {
                return AppDbContext.Set<TEntity>()
                    .AsNoTracking()
                    .ToList();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await AppDbContext.Set<TEntity>()
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                AppDbContext.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await AppDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                AppDbContext.Set<TEntity>().Update(entity);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public void Delete<TKey>(TKey id)
        {
            try
            {
                var entity = Get(id);
                if (entity is ISoftDelete<TUserId> softDeleteEntity)
                {
                    softDeleteEntity.IsDeleted = true;
                    softDeleteEntity.DeletedAt = DateTime.UtcNow;
                    softDeleteEntity.DeletedBy = CurrentUser.Id;

                    AppDbContext.Set<TEntity>().Update(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync<TKey>(
            TKey id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await GetAsync(id, cancellationToken);
                if (entity is ISoftDelete<TUserId> softDeleteEntity)
                {
                    softDeleteEntity.IsDeleted = true;
                    softDeleteEntity.DeletedAt = DateTime.UtcNow;
                    softDeleteEntity.DeletedBy = CurrentUser.Id;

                    AppDbContext.Set<TEntity>().Update(entity);
                }
                else
                {
                    await HardDeleteAsync(id, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public void HardDelete<TKey>(TKey id)
        {
            try
            {
                var entity = Get(id);
                if (entity != null)
                {
                    AppDbContext.Set<TEntity>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task HardDeleteAsync<TKey>(
            TKey id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await GetAsync(id, cancellationToken);
                if (entity != null)
                {
                    AppDbContext.Set<TEntity>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return AppDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await AppDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }
    }

    public class AppRepository<TEntity> : AppRepository<Guid, TEntity>
        where TEntity : EntityBase, new()
    {
        public AppRepository(
            ILogger<AppRepository<TEntity>> logger,
            AppDbContext<Guid> dbContext,
            ICurrentUser<Guid> currentUser) : base(logger, dbContext, currentUser)
        {
        }
    }

    public class AppRepositoryGuid<TEntity> : AppRepository<Guid, TEntity>
        where TEntity : EntityBase, new()
    {
        public AppRepositoryGuid(
            ILogger<AppRepositoryGuid<TEntity>> logger,
            AppDbContext<Guid> dbContext,
            ICurrentUser<Guid> currentUser) : base(logger, dbContext, currentUser)
        {
        }
    }
}