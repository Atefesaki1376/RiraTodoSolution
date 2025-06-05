namespace Rira.Todo.Application.Contracts.Dtos
{
    public abstract class DtoBase
    {
    }

    public abstract class DtoBase<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}