using Rira.Todo.Application.Services;

namespace Rira.Todo.Application
{
    public static class ApplicationModules
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMapperProfiles).Assembly);

            services.AddScoped<IAppSettingsService, AppSettingsService>();

            return services;
        }

        public static IServiceCollection AddCurrentUserServices<TUserId>(this IServiceCollection services)
            where TUserId : struct, IEquatable<TUserId>
        {
            services.AddScoped<ICurrentUser<TUserId>, CurrentUser<TUserId>>();
            return services;
        }



    }
}