namespace Rira.Todo.Application.Contracts.Dtos.TodoItems;

public class TodoItemDto : DtoBase<Guid>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}