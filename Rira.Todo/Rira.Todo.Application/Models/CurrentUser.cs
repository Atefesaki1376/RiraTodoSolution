namespace Rira.Todo.Application.Models
{
    public class CurrentUser<TKey> : ICurrentUser<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}