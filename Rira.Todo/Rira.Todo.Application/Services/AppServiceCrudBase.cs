namespace Rira.Todo.Application.Services
{
    public abstract class AppServiceCrudBase<TEntity, TDto> :
        AppServiceBase,
        IAppServiceCrudBase<TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
    {
        protected IMapper Mapper { get; init; }
        protected IRepository<TEntity> Repository { get; init; }

        protected AppServiceCrudBase(
            ILogger<AppServiceCrudBase<TEntity, TDto>> logger,
            IStringLocalizer<AppResource> localizer,
            IRepository<TEntity> repository,
            IMapper mapper) : base(logger, localizer)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<TDto> GetAsync(
            object id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await Repository.GetAsync(id, cancellationToken);
                if (entity is null)
                {
                    Logger.LogInformation(Localizer[MessageResource.NotFoundEntity], id);
                    return default!;
                }

                return Mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = await Repository.GetListAsync(cancellationToken);
                if (entities is null || !entities.Any())
                {
                    Logger.LogInformation(Localizer[MessageResource.NotFoundEntities]);
                    return Enumerable.Empty<TDto>();
                }

                return Mapper.Map<IEnumerable<TDto>>(entities);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<int> CreateAsync(
            TDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dto);

                await ValidateAsync(dto, ValidationAppContext.Create, cancellationToken);

                var entity = Mapper.Map<TEntity>(dto);
                await Repository.AddAsync(entity, cancellationToken);
                return await Repository.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<int> UpdateAsync<TKey>(
            TKey id,
            TDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(id);
                ArgumentNullException.ThrowIfNull(dto);

                await ValidateAsync(dto, ValidationAppContext.Update, cancellationToken);

                var entity = await Repository.GetAsync(id, cancellationToken);
                if (entity is null)
                {
                    Logger.LogInformation(Localizer[MessageResource.NotFoundEntity], id);
                    return 0;
                }

                var updatedEntity = UpdateMapper(dto, entity);
                Repository.Update(updatedEntity);
                return await Repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task DeleteAsync(
            object id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(id);

                var entity = await Repository.GetAsync(id, cancellationToken);
                if (entity is null)
                {
                    Logger.LogInformation(Localizer[MessageResource.NotFoundEntity], id);
                    return;
                }

                await Repository.DeleteAsync(id, cancellationToken);
                _ = await Repository.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, ex.Message);
                throw;
            }
        }

        protected virtual void BeforeUpdateMap(TDto dto, TEntity entity) { }
        protected virtual void AfterUpdateMap(TDto dto, TEntity entity) { }

        protected virtual TEntity UpdateMapper(TDto dto, TEntity entity)
        {
            BeforeUpdateMap(dto, entity);
            Mapper.Map(dto, entity);
            AfterUpdateMap(dto, entity);
            return entity;
        }

        protected abstract Task ValidateAsync(
            TDto dto,
            ValidationAppContext context,
            CancellationToken cancellationToken = default);
    }
}