namespace Rira.Todo.Domain.Shared.Interfaces
{
    public interface ISoftDelete<TUserId>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public TUserId? DeletedBy { get; set; }
    }
}