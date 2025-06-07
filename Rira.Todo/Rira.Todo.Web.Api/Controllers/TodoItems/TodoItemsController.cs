using Rira.Todo.Application.Contracts.TodoItems;

namespace Rira.Todo.Web.Api.Controllers.TodoItems
{
    public class TodoItemsController : AppControllerBase
    {

        private readonly ITodoItemAppService _todoService;

        public TodoItemsController
            (ILogger<TodoItemsController> logger,
            ITodoItemAppService todoService) : base(logger)
        {
            _todoService = todoService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetListAsync(CancellationToken cancellationToken = default)
        {
            var todos = await _todoService.GetListAsync(cancellationToken);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var todo = await _todoService.GetAsync(id, cancellationToken);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateAsync(TodoItemDto todo, CancellationToken cancellationToken = default)
        {
            var id = await _todoService.CreateAsync(todo, cancellationToken);
            todo.Id = id;
            return Created("get", new { id = todo.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            Guid id,
            TodoItemDto updatedTodo,
            CancellationToken cancellationToken = default)
        {
            await _todoService.UpdateAsync(id, updatedTodo, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _todoService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}