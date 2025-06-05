namespace Rira.Todo.Domain.Shared.Exceptions
{
    public abstract class AppException :
        Exception,
        IAppException
    {
        protected AppException(string message) : base(message)
        {
        }
    }
}