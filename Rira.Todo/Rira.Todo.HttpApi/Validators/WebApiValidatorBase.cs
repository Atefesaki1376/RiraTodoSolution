namespace Rira.Todo.HttpApi.Validators
{
    public abstract class WebApiValidatorBase<DTO> : AbstractValidator<DTO>
    {
        protected WebApiValidatorBase()
        {
            //ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");
        }
    }
}