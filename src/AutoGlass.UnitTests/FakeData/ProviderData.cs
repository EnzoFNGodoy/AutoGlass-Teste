using AutoGlass.Domain.Entities;

namespace AutoGlass.UnitTests.FakeData;

public sealed class ProviderData
{
    public Provider ValidProvider = new(
        description: new DescriptionData().ValidDescription,
        cnpj: new CNPJData().ValidCNPJ);

    public Provider InvalidProvider = new(
        description: new DescriptionData().InvalidDescription,
        cnpj: new CNPJData().InvalidCNPJ);
}