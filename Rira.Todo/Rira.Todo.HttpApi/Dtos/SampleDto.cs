namespace Rira.Todo.HttpApi.Dtos
{
    public class SampleDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = default!;
        public bool Completed { get; set; }
    }
}