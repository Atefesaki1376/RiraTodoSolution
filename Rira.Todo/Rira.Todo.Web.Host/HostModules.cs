namespace Rira.Todo.Web.Host
{
    public static class HostModules
    {
        public static IServiceCollection AddHostServices<TKey>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TKey : struct, IEquatable<TKey>
        {
            // Domain layer services
            services.AddDomainServices(configuration);

            // Ef core (database with user id type)
            services.AddEFCoreServices<TKey>(configuration);

            // add application layer services
            services.AddApplicationServices();
            services.AddCurrentUserServices<TKey>();

            // webapi layer services
            services.AddWebApiServices(configuration);

            

            return services;
        }

        public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}