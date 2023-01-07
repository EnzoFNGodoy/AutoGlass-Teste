using AutoGlass.Domain.Core.Entities;
using AutoGlass.Domain.ValueObjects;
using Flunt.Validations;

namespace AutoGlass.Domain.Entities;

public sealed class Product : Entity
{
    public Product(
        Description description, 
        DateTime productionDate, 
        DateTime expirationDate)
    {
        Description = description;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        IsActive = true;

        AddNotifications(Provider, Description, 
            new Contract<Product>()
            .Requires()
            .IsGreaterOrEqualsThan(ExpirationDate, ProductionDate, "Product.ExpirationDate", "The production date cannot be greather or equals than the expiration date")
            );
    }

    public Description Description { get; private set; }
    public DateTime ProductionDate { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; }

    public Provider Provider { get; private set; } 

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
}