﻿namespace Rira.Todo.Domain.Shared.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
