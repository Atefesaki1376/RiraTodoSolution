namespace Rira.Todo.Application.Contracts.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Get<TKey>(TKey id);

        Task<TEntity> GetAsync<TKey>(
            TKey id,
            CancellationToken cancellationToken = default);

        IEnumerable<TEntity> GetList();

        Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        void Add(TEntity entity);

        Task AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        Task DeleteAsync<TKey>(
            TKey id,
            CancellationToken cancellationToken = default);

        void Delete<TKey>(TKey id);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}