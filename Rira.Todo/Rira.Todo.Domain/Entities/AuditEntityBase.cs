namespace Rira.Todo.Domain.Entities
{
    public abstract class AuditEntityBase<TKey> : EntityBase<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public TKey CreatedBy { get; set; }
        public TKey ModifiedBy { get; set; }
    }
}