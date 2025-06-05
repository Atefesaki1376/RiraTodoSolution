namespace Rira.Todo.Domain.Shared.Interfaces
{
    public interface ICurrentUser<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        TKey Id { get; set; }
        string Name { get; set; }
        List<string> Roles { get; set; }
    }
}