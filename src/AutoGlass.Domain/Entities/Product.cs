using AutoGlass.Domain.Core.Entities;
using AutoGlass.Domain.ValueObjects;
using Flunt.Validations;

namespace AutoGlass.Domain.Entities;

public sealed class Product : Entity
{
    private Product() // Empty constructor for EF
    { }

    public Product(
        Description description,
        DateTime productionDate,
        DateTime expirationDate,
        bool isActive = true)
    {
        Description = description;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        IsActive = isActive;

        AddNotifications(Description,
            new Contract<Product>()
            .Requires()
            .IsGreaterOrEqualsThan(ExpirationDate, ProductionDate, "Product.ExpirationDate", "The production date cannot be greather or equals than the expiration date")
            );
    }

    public Description Description { get; private set; } = null!;
    public DateTime ProductionDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; }

    public Guid? ProviderId { get; private set; }
    public Provider? Provider { get; private set; }

    public void Activate()
    {
        if (IsValid)
            IsActive = true;
    }

    public void Deactivate()
    {
        if (IsValid)
            IsActive = false;
    }

    public void SetProvider(Provider provider)
    {
        AddNotifications(provider);

        if (IsValid && IsActive)
        {
            ProviderId = provider.Id;
            Provider = provider;
        }
    }
}