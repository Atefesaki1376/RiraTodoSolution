namespace Rira.Todo.Web.Api.Conventions
{
    public class DefaultResponseConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResultModel<object>), 200));
            action.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResultModel), 400));
        }
    }
}