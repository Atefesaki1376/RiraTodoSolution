namespace Rira.Todo.Application.Contracts.Validators
{
    public class ExceptionValidation : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ExceptionValidation(IDictionary<string, string[]> errors)
            : base("Validation failed")
        {
            Errors = errors;
        }
    }


}
