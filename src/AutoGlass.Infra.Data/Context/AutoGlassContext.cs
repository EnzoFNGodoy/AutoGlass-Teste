using AutoGlass.Domain.Entities;
using AutoGlass.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.Infra.Data.Context;

public sealed class AutoGlassContext : DbContext
{
    public AutoGlassContext(DbContextOptions options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    #region DbSets
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Provider> Providers => Set<Provider>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureMappings();

        base.OnModelCreating(modelBuilder);
    }
}