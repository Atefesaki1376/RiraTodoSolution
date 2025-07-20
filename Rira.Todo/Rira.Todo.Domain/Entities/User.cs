namespace Rira.Todo.Domain.Entities
{
    public class User : EntityBase
    {
        public User()
        {
            Todos = new HashSet<TodoItem>();
        }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string? ProfileImageUrl { get; set; } 
        public DateTime BirthDate { get; set; }
        public ICollection<TodoItem> Todos { get; protected set; }
    }
}
