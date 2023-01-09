using AutoGlass.Domain.Entities;
using AutoGlass.Domain.ValueObjects;

namespace AutoGlass.UnitTests.FakeData;

public sealed class ProductData
{
    private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public Product ValidActiveProduct = new(
            productId: Guid.NewGuid(),
            description: new DescriptionData().ValidDescription,
            productionDate: new DateTime(2023, 01, 07),
            expirationDate: new DateTime(2023, 01, 10)
            );

    public Product ValidInactiveProduct = new(
           productId: Guid.NewGuid(),
           description: new DescriptionData().ValidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 10),
           isActive: false
           );

    public Product InvalidActiveProduct = new(
           productId: Guid.NewGuid(),
           description: new DescriptionData().InvalidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 05)
           );

    public Product InvalidInactiveProduct = new(
           productId: Guid.NewGuid(),
           description: new DescriptionData().InvalidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 05),
           isActive: false
           );

    private readonly List<Product> _fakeProducts = new();

    public List<Product> GetFakeProducts(int quantity)
    {
        bool active = true;

        if (quantity > 10) quantity = 10;

        for (int i = 0; i < quantity; i++)
        {
            var random = new Random();
            var newDescription = new string(
                Enumerable.Repeat(CHARS, random.Next(5, 30))
                  .Select(s => s[random.Next(s.Length)])
                  .ToArray());

            if (i % 2 == 0) active = true;
            else active = false;

            var product = new Product(
                productId: Guid.NewGuid(),
                description: new Description(newDescription),
                productionDate: DateTime.Now.AddDays(i),
                expirationDate: DateTime.Now.AddDays(i + 10),
                isActive: active
                );

            product.SetProvider(new ProviderData().ValidProvider);

            _fakeProducts.Add(product);
        }

        return _fakeProducts;
    }
}