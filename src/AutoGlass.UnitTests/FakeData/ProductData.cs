using AutoGlass.Domain.Entities;

namespace AutoGlass.UnitTests.FakeData;

public sealed class ProductData
{
    public Product ValidActiveProduct = new(
            description: new DescriptionData().ValidDescription,
            productionDate: new DateTime(2023, 01, 07),
            expirationDate: new DateTime(2023, 01, 10)
            );

    public Product ValidInactiveProduct = new(
           description: new DescriptionData().ValidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 10),
           isActive: false
           );

    public Product InvalidActiveProduct = new(
           description: new DescriptionData().InvalidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 05)
           );

    public Product InvalidInactiveProduct = new(
           description: new DescriptionData().InvalidDescription,
           productionDate: new DateTime(2023, 01, 07),
           expirationDate: new DateTime(2023, 01, 05),
           isActive: false
           );
}