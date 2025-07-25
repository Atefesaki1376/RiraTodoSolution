﻿using Rira.Todo.Application.Contracts.Dtos.TodoItems;

namespace Rira.Todo.Application.Contracts.Interfaces.TodoItems;

public interface ITodoItemAppService
{
    Task<TodoItemDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TodoItemDto>> GetListAsync(CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(TodoItemDto todoItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(
        Guid todoItemId,
        TodoItemDto todoItem, 
        CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
