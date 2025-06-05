namespace Rira.Todo.Application.Contracts.Interfaces
{
    public interface IAppServiceCrudBase<TDto>
        where TDto : DtoBase, new()
    {
        Task<int> CreateAsync(
            TDto dto,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            object id,
            CancellationToken cancellationToken = default);
        Task<TDto> GetAsync(
            object id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TDto>> GetListAsync(CancellationToken cancellationToken = default);

        Task<int> UpdateAsync<TKey>(
            TKey id,
            TDto dto,
            CancellationToken cancellationToken = default);
    }
}