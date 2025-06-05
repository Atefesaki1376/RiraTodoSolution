namespace Rira.Todo.Application.Contracts.Validators
{
    public abstract class AppValidatorBase<Dto> : AbstractValidator<Dto>
    {
        protected AppValidatorBase()
        {
            //ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");
        }
    }
}