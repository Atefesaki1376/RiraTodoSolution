namespace Rira.Todo.Domain.Shared.Interfaces
{
    public interface IResultModel
    {
        string Error { get; }
        List<string> Errors { get; set; }
        bool IsSuccess { get; }
    }

    public interface IResultModel<TModel> : IResultModel
    {
        TModel Model { get; init; }
    }
}