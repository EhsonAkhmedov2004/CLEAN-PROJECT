using Microsoft.Extensions.Configuration;
using Infrastructure.Database;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
         
            options.UseNpgsql("Host=localhost; Port=5432; Database=postgres; Username=postgres;Password=501106020");

        });
        services.AddScoped<IDatabase>(provider => provider.GetRequiredService<DataContext>());
        return services;
    }


}