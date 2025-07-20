namespace Rira.Todo.Domain.Entities
{
    public class UserIdentity : EntityBase
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
