using Rira.Todo.Domain.Entities;

namespace Rira.Todo.Application.Contracts.Interfaces.Users
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserIdentity user);
    }
}
