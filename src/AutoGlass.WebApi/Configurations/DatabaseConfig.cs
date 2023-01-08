using AutoGlass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.WebApi.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<AutoGlassContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AutoGlassConnection")));
    }
}