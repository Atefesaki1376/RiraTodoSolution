using Rira.Todo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rira.Todo.Application.Contracts.Dtos.Users
{
    public class UserDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<TodoItem> Todos { get; protected set; }
    }
}
