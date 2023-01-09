using AutoGlass.Domain.Entities;

namespace AutoGlass.UnitTests.FakeData;

public sealed class ProviderData
{
    public Provider ValidProvider = new(
        providerId: Guid.NewGuid(),
        description: new DescriptionData().ValidDescription,
        cnpj: new CNPJData().ValidCNPJ);

    public Provider InvalidProvider = new(
        providerId: Guid.NewGuid(),
        description: new DescriptionData().InvalidDescription,
        cnpj: new CNPJData().InvalidCNPJ);
}