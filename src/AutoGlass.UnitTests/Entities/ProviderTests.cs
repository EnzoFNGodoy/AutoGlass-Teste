using AutoGlass.Domain.Entities;
using AutoGlass.Domain.ValueObjects;
using AutoGlass.UnitTests.FakeData;

namespace AutoGlass.UnitTests.Entities;

public sealed class ProviderTests
{
    [Fact]
    public void ShouldBe_Invalid_When_Description_IsInvalid()
    {
        var descriptionTest = new DescriptionData().InvalidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        Assert.False(provider.IsValid);
    }

    [Fact]
    public void ShouldBe_Invalid_When_CNPJ_IsInvalid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().InvalidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        Assert.False(provider.IsValid);
    }

    [Fact]
    public void ShouldBe_Valid_When_All_IsCorrect()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        Assert.True(provider.IsValid);
    }

    #region AddProduct
    [Fact]
    public void Should_AddProduct_When_Product_IsValid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var product = new ProductData().ValidActiveProduct;

        provider.AddProduct(product);

        Assert.Equal(1, provider.Products.Count);
        Assert.True(provider.Products.Any());
        Assert.True(product.IsValid);
        Assert.True(provider.IsValid);
    }

    [Fact]
    public void ShouldNot_AddProduct_When_Product_IsInvalid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var product = new ProductData().InvalidActiveProduct;

        provider.AddProduct(product);

        Assert.False(provider.Products.Any());
        Assert.False(product.IsValid);
        Assert.False(provider.IsValid);
    }

    [Fact]
    public void ShouldNot_AddProduct_When_Provider_IsInvalid()
    {
        var descriptionTest = new DescriptionData().InvalidDescription;
        var cnpjTest = new CNPJData().InvalidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var product = new ProductData().ValidActiveProduct;

        provider.AddProduct(product);

        Assert.False(provider.Products.Any());
        Assert.True(product.IsValid);
        Assert.False(provider.IsValid);
    }
    #endregion

    #region UpdateDescription
    [Fact]
    public void Should_UpdateDescription_When_Description_IsValid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var newDescription = new Description("Nova Descrição");

        provider.UpdateDescription(newDescription);

        Assert.Equal(newDescription.Text, provider.Description.Text);
        Assert.True(newDescription != descriptionTest);
        Assert.True(provider.IsValid);
    }

    [Fact]
    public void ShouldNot_UpdateDescription_When_Description_IsInvalid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().ValidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var newDescription = new DescriptionData().InvalidDescription;

        provider.UpdateDescription(newDescription);

        Assert.NotEqual(newDescription.Text, provider.Description.Text);
        Assert.False(provider.IsValid);
    }

    [Fact]
    public void ShouldNot_UpdateDescription_When_Provider_IsInvalid()
    {
        var descriptionTest = new DescriptionData().ValidDescription;
        var cnpjTest = new CNPJData().InvalidCNPJ;
        var provider = new Provider(
            providerId: Guid.NewGuid(),
            description: descriptionTest,
            cnpj: cnpjTest
            );

        var newDescription = new DescriptionData().ValidDescription;

        provider.UpdateDescription(newDescription);

        Assert.Equal(newDescription.Text, provider.Description.Text);
        Assert.False(provider.IsValid);
    }
    #endregion
}