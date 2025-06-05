namespace Rira.Todo.EFCore
{
    public static class EFCoreModules
    {
        public static IServiceCollection AddEFCoreServices<TUserId>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TUserId : struct, IEquatable<TUserId>
        {
            services.AddDbContext<AppDbContext<TUserId>>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDb"),
                 options => options.EnableRetryOnFailure()));

            services.AddScoped(typeof(IRepository<>), typeof(AppRepository<>));

            return services;
        }
    }
}