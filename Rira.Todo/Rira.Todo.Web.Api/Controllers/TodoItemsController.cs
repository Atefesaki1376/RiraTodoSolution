using Rira.Todo.Domain.Entities;

namespace Rira.Todo.Web.Api.Controllers
{
    public class TodoItemsController : AppControllerBase
    {
        private static readonly List<TodoItem> _todos = new();

        public TodoItemsController(ILogger<TodoItemsController> logger) : base(logger)
        {
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetListAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogInformation("user request get list");
            return Ok(_todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateAsync(TodoItem todo, CancellationToken cancellationToken = default)
        {
            todo.Id = Guid.NewGuid();
            _todos.Add(todo);
            Logger.LogInformation("user added new todoitem(s)");
            return CreatedAtAction(nameof(GetListAsync), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            Guid id, 
            TodoItem updatedTodo, 
            CancellationToken cancellationToken = default)
        {
            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null) return NotFound();

            existingTodo.Title = updatedTodo.Title;
            existingTodo.Description = updatedTodo.Description;
            existingTodo.IsCompleted = updatedTodo.IsCompleted;
            existingTodo.DueDate = updatedTodo.DueDate;
            Logger.LogInformation("user updated todoitem");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();
            _todos.Remove(todo);

            Logger.LogInformation("user deleted todoitem");
            return NoContent();
        }
    }
}