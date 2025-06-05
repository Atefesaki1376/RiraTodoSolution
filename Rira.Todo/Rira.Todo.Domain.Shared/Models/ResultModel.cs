namespace Rira.Todo.Domain.Shared.Models
{
    public class ResultModel : IResultModel
    {
        public bool IsSuccess => string.IsNullOrEmpty(Error);
        public List<string> Errors { get; set; } = new();
        public string Error => Errors?.FirstOrDefault() ?? "";

        public override string ToString()
        {
            if (IsSuccess) return $"successful";

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in Errors)
            {
                stringBuilder.AppendJoin(',', error);
            }
            return stringBuilder.ToString();
        }
    }

    public class ResultModel<TModel> : ResultModel, IResultModel<TModel>
    {
        public ResultModel(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; init; }
    }
}