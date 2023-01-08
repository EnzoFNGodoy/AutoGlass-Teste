using AutoGlass.Domain.ValueObjects;

namespace AutoGlass.UnitTests.FakeData;

public sealed class CNPJData
{
    public const string FORMATTED_CNPJ = "97.353.941/0001-40";
    public const string UNFORMATTED_CNPJ = "97353941000140";

    public CNPJ ValidCNPJ = new(UNFORMATTED_CNPJ);
    public CNPJ InvalidCNPJ = new("");
}