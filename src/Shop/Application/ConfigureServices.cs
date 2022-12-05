using System.Reflection;

using Application.Common.Authentication;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IAuth, Auth>();
            

            return services;
        }
    }
}