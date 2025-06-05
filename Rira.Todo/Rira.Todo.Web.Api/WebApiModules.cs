namespace Rira.Todo.Web.Api
{
    public static class WebApiModules
    {
        public static IServiceCollection AddWebApiServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new DefaultResponseConvention());
            });

            return services;
        }
    }
}