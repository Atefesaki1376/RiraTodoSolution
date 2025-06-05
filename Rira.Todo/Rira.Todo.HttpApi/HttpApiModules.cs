namespace Rira.Todo.HttpApi
{
    public static class HttpApiModules
    {
        public static IServiceCollection AddHttpApiDefaultServices(
            this IServiceCollection services,
            IConfiguration? configuration = null)
        {
            services.AddScoped<IResultModel, ResultModel>();
            services.AddScoped(typeof(IResultModel<>), typeof(ResultModel<>));
            services.AddScoped<IApiExceptionHandler, ApiExceptionHandler>();
            services.AddScoped<IApiResponse, ApiResponse>();

            services.AddScoped<SampleService>();

            return services;
        }
    }
}