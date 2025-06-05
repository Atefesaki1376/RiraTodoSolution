namespace Rira.Todo.Domain.Entities
{
    public abstract class EntityBase
    {
    }

    public abstract class EntityBase<TKey> : EntityBase
        where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}