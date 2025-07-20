using Rira.Todo.Application.Contracts.Interfaces.TodoItems;
using Rira.Todo.Application.Contracts.Interfaces.Users;
using Rira.Todo.Web.Api.Controllers.TodoItems;

namespace Rira.Todo.Web.Api.Controllers.Users
{
    public class UserController : AppControllerBase
    {
        private readonly IUserAppService _userService;
        public UserController
           (ILogger<UserController> logger,
            IUserAppService userService) : base(logger)
        {
            _userService = userService;
        }
    }
}
