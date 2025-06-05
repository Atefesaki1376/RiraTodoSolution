namespace Rira.Todo.Domain.Shared
{
    public static class DomainModules
    {
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddLocalization();

            return services;
        }
    }
}
