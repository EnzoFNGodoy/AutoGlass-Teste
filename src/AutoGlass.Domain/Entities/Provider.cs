using AutoGlass.Domain.Core.Entities;
using AutoGlass.Domain.ValueObjects;
using Flunt.Validations;

namespace AutoGlass.Domain.Entities;

public sealed class Provider : Entity
{
    private readonly IList<Product>? _products;

    private Provider() // Empty constructor for EF
    { }

    public Provider(Description description, CNPJ cnpj)
    {
        Description = description;
        CNPJ = cnpj;

        _products = new List<Product>();

        AddNotifications(Description, CNPJ);
    }

    public Description Description { get; private set; } = null!;
    public CNPJ CNPJ { get; private set; } = null!;

    public IReadOnlyCollection<Product> Products { get => _products!.ToArray(); }

    public void AddProduct(Product product)
    {
        AddNotifications(product);

        if (IsValid)
            _products!.Add(product);
    }
}