using Rira.Todo.Application.Contracts.TodoItems;
using Rira.Todo.Application.Services;

namespace Rira.Todo.Application.TodoItems
{
    public class TodoItemAppService : AppServiceBase, ITodoItemAppService
    {
        private readonly IRepository<TodoItem> _todoItemrepository;
        private readonly IMapper _mapper;

        private readonly IValidator<TodoItemDto> _validator;

        public TodoItemAppService(
            ILogger<TodoItemAppService> logger,
            IRepository<TodoItem> todoItemrepository,
            IStringLocalizer<AppResource> localizer,
            IValidator<TodoItemDto> validator,
            IMapper mapper) : base(logger, localizer)
        {
            _todoItemrepository = todoItemrepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> CreateAsync(
            TodoItemDto todoItem,
            CancellationToken cancellationToken = default)
        {
            try
            {

                await _validator.ValidateAndThrowAsync(todoItem, cancellationToken);


                TodoItem todo = _mapper.Map<TodoItem>(todoItem);


                await _todoItemrepository.AddAsync(todo, cancellationToken);
                await _todoItemrepository.SaveChangesAsync(cancellationToken);


                return todo.Id;
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                      .GroupBy(e => e.PropertyName)
                      .ToDictionary(
                       g => g.Key,
                       g => g.Select(e => e.ErrorMessage).ToArray());

                throw new ExceptionValidation(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occured, {Message}", ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _todoItemrepository.DeleteAsync(id, cancellationToken);
                await _todoItemrepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occured, {Message}", ex.Message);
                throw;
            }

        }

        public async Task<TodoItemDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                TodoItem todoItem = await _todoItemrepository.GetAsync(id, cancellationToken);
                await _todoItemrepository.SaveChangesAsync(cancellationToken);
                return _mapper.Map<TodoItemDto>(todoItem);
       
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occured, {Message}", ex.Message);
                throw;
            }

        }

        public async Task<List<TodoItemDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                IEnumerable<TodoItem> todoItems = await _todoItemrepository.GetListAsync(cancellationToken);
                await _todoItemrepository.SaveChangesAsync(cancellationToken);
                return _mapper.Map<List<TodoItemDto>>(todoItems);
              
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occured, {Message}", ex.Message);
                throw;
            }

        }

        public async Task UpdateAsync(
            Guid todoItemId,
            TodoItemDto todoItem,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(todoItem, cancellationToken);
                TodoItem todo = _mapper.Map<TodoItem>(todoItem);
                _todoItemrepository.Update(todo);
                await _todoItemrepository.SaveChangesAsync(cancellationToken);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                      .GroupBy(e => e.PropertyName)
                      .ToDictionary(
                       g => g.Key,
                       g => g.Select(e => e.ErrorMessage).ToArray());

                throw new ExceptionValidation(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError("An error occured, {Message}", ex.Message);
                throw;
            }

        }


    }
}
