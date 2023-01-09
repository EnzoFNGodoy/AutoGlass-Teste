using AutoGlass.Domain.Entities;
using AutoGlass.UnitTests.FakeData;

namespace AutoGlass.UnitTests.Entities;

public sealed class ProductTests
{
    [Fact]
    public void ShouldBe_Invalid_When_Description_IsInvalid()
    {
        var descriptionTest = new DescriptionData().InvalidDescription;
        var product = new Product(
            productId: Guid.NewGuid(),
            description: descriptionTest,
            productionDate: new DateTime(2023, 01, 07),
            expirationDate: new DateTime(2023, 01, 10)
            );

        Assert.False(product.IsValid);
    }

    [Fact]
    public void ShouldBe_Invalid_When_ExpirationDate_IsLowerOrEqualsThan_ProductionDate()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var product = new Product(
            productId: Guid.NewGuid(),
            description: descriptionTest,
            productionDate: new DateTime(2023, 01, 07),
            expirationDate: new DateTime(2023, 01, 05)
            );

        Assert.False(product.IsValid);
    }

    [Fact]
    public void ShouldBe_Valid_When_All_IsCorrect()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var product = new Product(
            productId: Guid.NewGuid(),
            description: descriptionTest,
            productionDate: new DateTime(2023, 01, 07),
            expirationDate: new DateTime(2023, 01, 10)
            );

        Assert.True(product.IsValid);
    }

    #region Activate
    [Fact]
    public void Activate_When_IsValid()
    {
        var product = new ProductData().ValidInactiveProduct;
        product.Activate();


        Assert.True(product.IsValid);
        Assert.True(product.IsActive);
    }

    [Fact]
    public void NotActivate_When_IsInvalid()
    {
        var product = new ProductData().InvalidInactiveProduct;
        product.Activate();

        Assert.False(product.IsValid);
        Assert.False(product.IsActive);
    }
    #endregion

    #region Deactivate

    [Fact]
    public void Deactivate_When_IsValid()
    {
        var product = new ProductData().ValidActiveProduct;
        product.Deactivate();

        Assert.True(product.IsValid);
        Assert.False(product.IsActive);
    }

    [Fact]
    public void NotDeactivate_When_IsInvalid()
    {
        var product = new ProductData().InvalidActiveProduct;
        product.Deactivate();

        Assert.False(product.IsValid);
        Assert.True(product.IsActive);
    }
    #endregion

    #region SetProvider
    [Fact]
    public void SetProvider_When_IsValid()
    {
        var product = new ProductData().ValidActiveProduct;
        var provider = new ProviderData().ValidProvider;
        product.SetProvider(provider);

        Assert.Equal(provider, product.Provider);
        Assert.Equal(provider.Id, product.ProviderId);
        Assert.True(product.IsValid);
    }

    [Fact]
    public void NotSetProvider_When_IsInactive()
    {
        var product = new ProductData().ValidInactiveProduct;
        var provider = new ProviderData().ValidProvider;
        product.SetProvider(provider);

        Assert.Null(product.Provider);
        Assert.Null(product.ProviderId);
        Assert.False(product.IsActive);
        Assert.True(product.IsValid);
    }

    [Fact]
    public void NotSetProvider_When_Product_IsInvalid()
    {
        var product = new ProductData().InvalidActiveProduct;
        var provider = new ProviderData().ValidProvider;
        product.SetProvider(provider);

        Assert.Null(product.Provider);
        Assert.Null(product.ProviderId);
        Assert.False(product.IsValid);
    }

    [Fact]
    public void NotSetProvider_When_Provider_IsInvalid()
    {
        var product = new ProductData().ValidActiveProduct;
        var provider = new ProviderData().InvalidProvider;
        product.SetProvider(provider);

        Assert.False(provider.IsValid);
        Assert.Null(product.Provider);
        Assert.Null(product.ProviderId);
        Assert.False(product.IsValid);
    }
    #endregion
}