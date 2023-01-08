using AutoGlass.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.Infra.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureMappings(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new ProviderMap());
    }
}