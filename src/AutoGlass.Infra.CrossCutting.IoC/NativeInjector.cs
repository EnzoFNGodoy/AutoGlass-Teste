using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Services;
using AutoGlass.Domain.Interfaces;
using AutoGlass.Infra.Data.Context;
using AutoGlass.Infra.Data.Repositories;
using AutoGlass.Infra.Data.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace AutoGlass.Infra.CrossCutting.IoC;

public static class NativeInjector
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<AutoGlassContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IProductServices, ProductServices>();
    }
}