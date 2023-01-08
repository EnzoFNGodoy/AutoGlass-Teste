using AutoGlass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGlass.Infra.Data.Mappings;

public sealed class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Provider)
            .WithMany(prov => prov.Products)
            .HasForeignKey(p => p.ProviderId);

        builder.OwnsOne(p => p.Description, description =>
        {
            description.Property(d => d.Text)
            .HasColumnName("Description")
            .HasColumnType("varchar(100)");

            description.Ignore(d => d.Notifications);
        });

        builder.Property(p => p.ProductionDate)
            .HasColumnName("ProductionDate")
            .HasColumnType("datetime");

        builder.Property(p => p.ExpirationDate)
            .HasColumnName("ExpirationDate")
            .HasColumnType("datetime");

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("bit");

        builder.Ignore(p => p.Notifications);
    }
}