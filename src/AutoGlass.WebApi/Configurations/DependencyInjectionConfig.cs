using AutoGlass.Infra.CrossCutting.IoC;

namespace AutoGlass.WebApi.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.RegisterServices();
    }
}