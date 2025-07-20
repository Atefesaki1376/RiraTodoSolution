using Rira.Todo.Application.Contracts.Dtos.Users;

namespace Rira.Todo.Application.Contracts.Interfaces.Users
{
    public interface IUserAppService 
    {
        Task<UserDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<UserDto>> GetListAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(UserDto user, CancellationToken cancellationToken = default);
        Task UpdateAsync(
            Guid userId,
            UserDto user,
            CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
