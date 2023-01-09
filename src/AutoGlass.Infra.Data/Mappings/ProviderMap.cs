using AutoGlass.Domain.Entities;
using AutoGlass.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGlass.Infra.Data.Mappings;

public sealed class ProviderMap : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.Description, description =>
        {
            description.Property(d => d.Text)
            .HasColumnName("Description")
            .HasColumnType("varchar(100)");

            description.Ignore(d => d.Notifications);
        });

        builder.OwnsOne(p => p.CNPJ, cnpj =>
        {
            cnpj.Property(c => c.Number)
            .HasColumnName("CNPJ")
            .HasColumnType("varchar(14)");

            cnpj.HasIndex(c => c.Number).IsUnique();

            cnpj.Ignore(c => c.Notifications);
        });

        builder.Ignore(p => p.Notifications);
    }
}