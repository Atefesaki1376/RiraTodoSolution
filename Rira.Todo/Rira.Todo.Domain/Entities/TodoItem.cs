﻿namespace Rira.Todo.Domain.Entities;

public class TodoItem : AuditEntityBase<Guid>
{
    public string Title { get; set; } 
    public string? Description { get; set; } 
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}