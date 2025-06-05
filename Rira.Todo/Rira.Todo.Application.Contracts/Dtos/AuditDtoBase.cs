namespace Rira.Todo.Application.Contracts.Dtos
{
    public abstract class AuditDtoBase<TKey> : DtoBase<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public TKey CreatedBy { get; set; }
        public TKey ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}