using Rira.Todo.Application.Contracts.Dtos.TodoItems;

namespace Rira.Todo.Application.Contracts.Validators
{
    public class TodoItemDtoValidator : AbstractValidator<TodoItemDto>
    {
        public TodoItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.DueDate)
                .NotEmpty();
        }
    }
}